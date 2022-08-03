// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi, PlanetXamarin
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Humanizer;
using Microsoft.Extensions.Caching.Memory;
using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Brokers.Gravatars;
using PlanetDotnet.Api.Brokers.Loggings;
using PlanetDotnet.Api.Extensions;
using PlanetDotnet.Api.Models.Foundations.Authors.Exceptions;
using PlanetDotnet.Api.Models.Foundations.Feeds.Exceptions;
using PlanetDotnet.Api.Models.Foundations.Previews;
using PlanetDotnet.Shared.Abstractions;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace PlanetDotnet.Api.Services.Foundations.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient httpClient;

        private readonly IAuthorBroker authorBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IGravatarBroker gravatarBroker;

        private static AsyncRetryPolicy retryPolicy;
        private static AsyncCachePolicy cachePolicy;

        public AuthorService(
            IAuthorBroker authorBroker,
            ILoggingBroker loggingBroker,
            IGravatarBroker gravatarBroker)
        {
            this.authorBroker = authorBroker;
            this.loggingBroker = loggingBroker;
            this.gravatarBroker = gravatarBroker;

            if (retryPolicy == null)
            {
                // cache in memory for an hour
                var memoryCache = new MemoryCache(new MemoryCacheOptions());
                var memoryCacheProvider = new MemoryCacheProvider(memoryCache);
                cachePolicy = Policy.CacheAsync(memoryCacheProvider, TimeSpan.FromHours(1), OnCacheError);

                // retry policy with max 2 retries, delay by x*x^1.2 where x is retry attempt
                // this will ensure we don't retry too quickly
                retryPolicy = Policy.Handle<FailedFeedServiceException>()
                    .WaitAndRetryAsync(2, retry => TimeSpan.FromSeconds(retry * Math.Pow(1.2, retry)));
            }

            if (this.httpClient == null)
            {
                this.httpClient = new HttpClient();
                this.httpClient.DefaultRequestHeaders.UserAgent.Add(
                    new ProductInfoHeaderValue("PlanetDotnet", $"{GetType().Assembly.GetName().Version}"));
                this.httpClient.Timeout = TimeSpan.FromSeconds(15);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            }
        }

        public IEnumerable<IAmACommunityMember> RetrieveAllAuthors()
        {
            try
            {
                var authors = this.authorBroker.SelectAllAuthers().ToList();

                foreach (var author in authors)
                {
                    author.Avatar =
                        this.gravatarBroker.GetGravatarImage(
                            member: author);
                }

                return authors;
            }
            catch (Exception exception)
            {
                var authorServiceException =
                    new AuthorServiceException(exception);

                this.loggingBroker.LogError(authorServiceException);

                throw authorServiceException;
            }
        }

        public async ValueTask<IEnumerable<PreviewItem>> RetrieveAllPreviewsAsync()
        {
            try
            {
                var authors = RetrieveAllAuthors();
                var feed = await RetrieveFeedAsync(400);

                var previewItems = new List<PreviewItem>();
                foreach (var item in feed.Items)
                {
                    var author = authors.FirstOrDefault(a => MatchesAuthorUrls(a, item.Links.Select(l => l.Uri), item));

                    string authorName;

                    if (author != null)
                    {
                        authorName = $"{author.FirstName} {author.LastName}".Trim();
                    }
                    // If no author was matched, extract the name from the RSS feed, something is better than nothing right?!
                    else
                    {
                        var creator = item.ElementExtensions.FirstOrDefault(x => x.OuterName == "creator" && x.OuterNamespace == "http://purl.org/dc/elements/1.1/");
                        if (creator != null)
                        {
                            authorName = creator.GetObject<XmlElement>().Value ?? string.Empty;
                        }
                        else
                        {
                            authorName = string.Join(", ", item.Authors.Select(a => $"{a.Name} {a.Email}".Trim()));
                        }
                    }

                    var link = item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty;
                    var html = item.Content?.ToHtml() ?? item.Summary?.ToHtml() ?? string.Empty;

                    var previewItem = new PreviewItem
                    {
                        AuthorName = authorName,
                        Gravatar = author?.Avatar,
                        Title = item.Title.Text,
                        Link = link,
                        Body = html.Sanitize(),
                        PublishDate = item.PublishDate.Humanize()
                    };

                    previewItems.Add(previewItem);
                }

                return previewItems;
            }
            catch (Exception exception)
            {
                var authorServiceException =
                    new AuthorServiceException(exception);

                this.loggingBroker.LogError(authorServiceException);

                throw authorServiceException;
            }
        }

        public Task<SyndicationFeed> RetrieveFeedAsync(
            int? numberOfItems,
            string tag = "all",
            string languageCode = "mixed")
        {
            return cachePolicy.ExecuteAsync(context =>
                LoadFeedInternalAsync(numberOfItems, tag, languageCode),
                new Context($"feed{numberOfItems}{languageCode}"));
        }

        public async ValueTask<string> RetrieveXmlFeedAsync(
           int? numberOfItems,
           string tag = "all",
           string languageCode = "mixed")
        {
            var feed = await RetrieveFeedAsync(
                numberOfItems,
                tag,
                languageCode);

            return ToXml(feed);
        }

        private async Task<SyndicationFeed> LoadFeedInternalAsync(
            int? numberOfItems,
            string tag = "all",
            string languageCode = "mixed")
        {
            IEnumerable<IAmACommunityMember> authors
                = RetrieveAllAuthors();

            IEnumerable<IAmACommunityMember> filteredAuthors;

            if (string.IsNullOrWhiteSpace(languageCode)
                || languageCode == "mixed")
            {
                filteredAuthors = tag == "all"
                    ? authors
                    : authors.Where(author => author.Tags.Contains(tag));
            }
            else
            {
                filteredAuthors = authors.Where(author =>
                    CultureInfo.CreateSpecificCulture(author.FeedLanguageCode).Name == languageCode
                    && author.Tags != null
                    && author.Tags.Contains(tag));
            }

            var feedTasks = filteredAuthors.SelectMany(t =>
                TryReadFeeds(t, GetFilterFunction(t)));

            var syndicationItems =
                await Task.WhenAll(feedTasks).ConfigureAwait(false);

            var combinedFeed = GetCombinedFeed(
                syndicationItems.SelectMany(f => f),
                languageCode,
                filteredAuthors,
                numberOfItems);

            return combinedFeed;
        }

        private SyndicationFeed GetCombinedFeed(
            IEnumerable<SyndicationItem> items,
            string languageCode,
            IEnumerable<IAmACommunityMember> authors,
            int? numberOfItems)
        {
            DateTimeOffset GetMaxTime(SyndicationItem item)
            {
                return new[] { item.PublishDate.UtcDateTime, item.LastUpdatedTime.UtcDateTime }.Max();
            }

            var orderedItems = items
                .Where(item =>
                    GetMaxTime(item) <= DateTimeOffset.UtcNow)
                .OrderByDescending(item => GetMaxTime(item));

            var feed = new SyndicationFeed(
                "Planet .NET",
                "An aggregated feed from the .NET community",
                new Uri("https://github.com/mabroukmahdhi/planet.net"),
                numberOfItems.HasValue ? orderedItems.Take(numberOfItems.Value) : orderedItems)
            {
                ImageUrl = new Uri("https://github.com/mabroukmahdhi/planet.net/blob/main/Assets/Badge/Logo.png"),
                Copyright = new TextSyndicationContent("The copyright for each post is retained by its author."),
                Language = languageCode,
                LastUpdatedTime = DateTimeOffset.UtcNow
            };

            foreach (var author in authors)
            {
                feed.Contributors.Add(new SyndicationPerson(
                    author.EmailAddress, $"{author.FirstName} {author.LastName}", author.WebSite.ToString()));
            }

            return feed;
        }

        private IEnumerable<Task<IEnumerable<SyndicationItem>>> TryReadFeeds(IAmACommunityMember author, Func<SyndicationItem, bool> filter)
        {
            return author.FeedUris.Select(uri => TryReadFeed(author, uri.AbsoluteUri, filter));
        }

        private async Task<IEnumerable<SyndicationItem>> TryReadFeed(IAmACommunityMember author, string feedUri, Func<SyndicationItem, bool> filter)
        {
            try
            {
                return await retryPolicy.ExecuteAsync(context => ReadFeed(feedUri, filter), new Context(feedUri)).ConfigureAwait(false);
            }
            catch (FailedFeedServiceException ex)
            {
                var failedFeedServiceException =
                    new FailedFeedServiceException(
                        $"{author.FirstName} {author.LastName}'s feed of {ex.Data["FeedUri"]} failed to load.",
                        ex);

                this.loggingBroker.LogError(failedFeedServiceException);
            }

            return new SyndicationItem[0];
        }

        private async Task<IEnumerable<SyndicationItem>> ReadFeed(string feedUri, Func<SyndicationItem, bool> filter)
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync(feedUri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    using (var feedStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var reader = XmlReader.Create(feedStream))
                    {
                        var feed = SyndicationFeed.Load(reader);
                        var filteredItems = feed.Items
                            .Where(item => TryFilter(item, filter));

                        return filteredItems;
                    }
                }
            }
            catch (HttpRequestException hex)
            {
                throw new FailedFeedServiceException("Loading remote syndication feed failed", hex)
                    .WithData("FeedUri", feedUri);
            }
            catch (WebException ex)
            {
                throw new FailedFeedServiceException("Loading remote syndication feed timed out", ex)
                    .WithData("FeedUri", feedUri);
            }
            catch (XmlException ex)
            {
                throw new FailedFeedServiceException("Failed parsing remote syndication feed", ex)
                    .WithData("FeedUri", feedUri);
            }
            catch (TaskCanceledException ex)
            {
                throw new FailedFeedServiceException("Reading feed timed out", ex)
                    .WithData("FeedUri", feedUri);
            }
            catch (OperationCanceledException opcex)
            {
                throw new FailedFeedServiceException("Reading feed timed out", opcex)
                    .WithData("FeedUri", feedUri);
            }

            throw new FailedFeedServiceException("Loading remote syndication feed failed.")
                .WithData("FeedUri", feedUri)
                .WithData("HttpStatusCode", (int)response.StatusCode);
        }

        private static Func<SyndicationItem, bool> GetFilterFunction(IAmACommunityMember author)
        {
            if (author is IFilterMyBlogPosts filterMyBlogPosts)
            {
                return filterMyBlogPosts.Filter;
            }

            return null;
        }

        private void OnCacheError(Context arg1, string arg2, Exception arg3)
        {
            var failedFeedServiceException =
                new FailedFeedServiceException($"Failed caching item: {arg1} - {arg2}", arg3);

            this.loggingBroker.LogError(failedFeedServiceException);
        }

        private static bool TryFilter(SyndicationItem item, Func<SyndicationItem, bool> filterFunc)
        {
            try
            {
                if (filterFunc != null)
                    return filterFunc(item);
            }
            catch (Exception)
            {
            }

            // the authors' filter is derped or has no filter
            // try some sane defaults
            return item.ApplyDefaultFilter();
        }

        private static string ToXml(SyndicationFeed feed)
        {
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);

            var rssFormatter = new Rss20FeedFormatter(feed);

            rssFormatter.WriteTo(xmlWriter);

            return stringWriter.ToString();
        }

        private static bool MatchesAuthorUrls(IAmACommunityMember author, IEnumerable<Uri> urls, SyndicationItem item)
        {
            var authorHosts = author.FeedUris.Select(au => au.Host.ToLowerInvariant()).Concat(new[] { author.WebSite.Host.ToLowerInvariant() }).ToArray();
            var feedBurnerAuthors = author.FeedUris.Where(au => au.Host.Contains("feeds.feedburner")).ToList();
            var mediumAuthors = author.FeedUris.Where(au => au.Host.Contains("medium.com")).ToList();
            var youtubeAuthors = author.FeedUris.Where(au => au.Host.Contains("youtube.com")).ToList();

            foreach (var itemUrl in urls)
            {
                var host = itemUrl.Host.ToLowerInvariant();

                if (host.Contains("medium.com"))
                {
                    if (itemUrl.Segments.Count() >= 3)
                    {
                        var mediumId = itemUrl.Segments[1].Trim('/');
                        return mediumAuthors.Any(fba => fba.AbsoluteUri.Contains(mediumId));
                    }
                }

                if (host.Contains("feedproxy.google")) //  feed burner is messed up :(
                {
                    // url will look like:
                    // feedproxy.google.com/~r/<feedburnerId>/~3/bgJNuDXwkU0/O
                    if (itemUrl.Segments.Count() >= 5)
                    {
                        var feedBurnerId = itemUrl.Segments[2].Trim('/');
                        return feedBurnerAuthors.Any(fba => fba.AbsoluteUri.Contains(feedBurnerId));
                    }
                }

                if (host.Contains("youtube.com")) //need to match youtube channel
                {
                    var channel = item?.Authors?.FirstOrDefault()?.Uri;
                    if (channel == null)
                        return false;

                    var id = channel.Replace("https://www.youtube.com/channel/", string.Empty);

                    return youtubeAuthors.Any(yt => yt.AbsoluteUri.Contains(id));
                }

                if (authorHosts.Contains(host))
                    return true;

                if (authorHosts.Contains(host.Replace("www.", "")))
                    return true;

                if (authorHosts.Contains(host.Insert(0, "www.")))
                    return true;
            }

            return false;
        }
    }
}
