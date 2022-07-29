// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Models.Apis.FeedRequests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace PlanetDotnet.Api.Services
{
    public class FeedService
    {
        public async ValueTask<string> CreateAndLoadFeedAsync(
            FeedRequest feedRequest)
        {
            var feedUris = feedRequest.Authors?
                .SelectMany(f => f.FeedUris)?
                .Distinct();

            if (feedUris == null)
                return string.Empty;


            List<SyndicationFeed> feeds = new();

            using var httpClient = new HttpClient();

            foreach (string feedUri in feedUris)
            {
                var feed = await LoadOneFeedAsync(httpClient, feedUri);
                if (feed == null)
                    continue;

                feeds.Add(feed);
            }

            var sItems = feeds.SelectMany(f => f.Items);

            var planetFeed = GetCombinedFeed(sItems, feedRequest.Authors, "mixed", 150);

            return ToXml(planetFeed);
        }

        private async ValueTask<SyndicationFeed> LoadOneFeedAsync(
            HttpClient httpClient,
            string uri)
        {
            using HttpResponseMessage response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                using var feedStream = await response.Content.ReadAsStreamAsync();
                using var reader = XmlReader.Create(feedStream);
                var feed = SyndicationFeed.Load(reader);
                return feed;
            }
            return null;
        }

        private SyndicationFeed GetCombinedFeed(
            IEnumerable<SyndicationItem> items,
            IEnumerable<AuthorInfo> authorInfos,
            string languageCode,
            int? numberOfItems)
        {
            DateTimeOffset GetMaxTime(SyndicationItem item)
            {
                return new[]
                {
                    item.PublishDate.UtcDateTime,
                    item.LastUpdatedTime.UtcDateTime
                }.Max();
            }

            var orderedItems = items
                .Where(item =>
                    GetMaxTime(item) <= DateTimeOffset.UtcNow)
                .OrderByDescending(item => GetMaxTime(item));

            var feed = new SyndicationFeed(
                "Planet .NET",
                "An aggregated feed from the .NET community",
                new Uri("https://brave-field-0981c0d03.1.azurestaticapps.net"),
                numberOfItems.HasValue ? orderedItems.Take(numberOfItems.Value) : orderedItems)
            {
                ImageUrl = new Uri("https://brave-field-0981c0d03.1.azurestaticapps.net/img/logo.png"),

                Copyright = new TextSyndicationContent(
                    text: "The copyright for each post is retained by its author."),

                Language = languageCode,

                LastUpdatedTime = DateTimeOffset.UtcNow
            };

            foreach (var author in authorInfos)
            {
                feed.Contributors.Add(
                    new SyndicationPerson(
                        email: author.Email,
                        name: author.FullName,
                        uri: author.WebSite));
            }

            return feed;
        }

        private static string ToXml(SyndicationFeed feed)
        {
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);

            var rssFormatter = new Rss20FeedFormatter(feed);

            rssFormatter.WriteTo(xmlWriter);

            return stringWriter.ToString();
        }
    }
}
