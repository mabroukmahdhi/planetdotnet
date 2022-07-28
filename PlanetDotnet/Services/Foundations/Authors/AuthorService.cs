// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Apis;
using PlanetDotnet.Brokers.Authors;
using PlanetDotnet.Brokers.Gravatars;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Models.Apis.FeedRequests;
using PlanetDotnet.Models.Foundations.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Authors
{
    public partial class AuthorService : IAuthorService
    {
        private readonly IApiBroker apiBroker;
        private readonly IAuthorBroker authorBroker;
        private readonly IGravatarBroker gravatarBroker;
        private readonly ILoggingBroker loggingBroker;

        public AuthorService(
            IApiBroker apiBroker,
            IAuthorBroker authorBroker,
            IGravatarBroker gravatarBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.authorBroker = authorBroker;
            this.gravatarBroker = gravatarBroker;
            this.loggingBroker = loggingBroker;
        }

        public IEnumerable<IAmACommunityMember> RetrieveAllAuthers()
        {
            return this.authorBroker.SelectAllAuthers();
        }

        public string RetrieveAuthorImage(IAmACommunityMember author) =>
        TryCatch(() =>
        {
            ValidateAuthor(author);

            return this.gravatarBroker.GetGravatarImage(author);
        });

        public async void PostFeeds()
        {
            var authors = this.authorBroker.SelectAllAuthers();

            var feedRquest = new FeedRequest
            {
                Authors = GenerateAuthorInfos(authors),
                MaxItems = 0,
                FeedLanguage = "mixed",
                Tags = new List<string>()
            };

            await this.apiBroker.PostFeedAsync(feedRquest);
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
