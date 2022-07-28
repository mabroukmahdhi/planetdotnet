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

        public async ValueTask PostFeedsAsync()
        {
           var authors = this.authorBroker.SelectAllAuthers();

            var feedRequest = new FeedRequest
            {
                
            };

            await this.apiBroker.PostFeedAsync(null);
        }
    }
}
