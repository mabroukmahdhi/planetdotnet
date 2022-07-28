// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using PlanetDotnet.Models.Apis.FeedRequests;
using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Foundations.Configurations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Feeds
{
    public class FeedBroker : IFeedBroker
    {
        private readonly LocalConfigurations localConfigurations;
        private readonly HttpClient httpClient;

        public FeedBroker(
            IConfiguration configuration,
            HttpClient httpClient)
        {
            this.localConfigurations =
                configuration.Get<LocalConfigurations>();

            this.httpClient = httpClient;
        }

        public async ValueTask CeateFeedFileAsync(
            IEnumerable<IAmACommunityMember> authors)
        {
            var feedRquest = new FeedRequest
            {
                Authors = GenerateAuthorInfos(authors),
                MaxItems = 0,
                FeedLanguage = "mixed",
                Tags = new List<string>()
            };

            using var response = await httpClient.PostAsJsonAsync(
                 requestUri: this.localConfigurations.FeedApiUrl,
                 value: feedRquest);

            var content = await response.Content.ReadAsStringAsync();

            File.WriteAllText(
                path: this.localConfigurations.FeedFilePath,
                contents: content);
        }

        private static IEnumerable<AuthorInfo> GenerateAuthorInfos(
            IEnumerable<IAmACommunityMember> authors)
        {
            foreach (var author in authors)
            {
                yield return new AuthorInfo
                {
                    FullName = $"{author.FirstName} {author.LastName}",
                    Email = author.EmailAddress,
                    FeedUris = author.FeedUris?.Select(f => f.ToString()),
                    Language = author.FeedLanguageCode,
                    WebSite = author.WebSite.ToString()
                };
            }
        }
    }
}
