// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Apis;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Authors
{
    public partial class AuthorService : IAuthorService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public AuthorService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<IEnumerable<IAmACommunityMember>> RetrieveAllAuthorsAsync()
        {
            return await this.apiBroker.GetAuthorsAsync();
        }

        public async ValueTask<IEnumerable<IAmACommunityMember>> RetrieveAllPodcastsAsync()
        {
            return await this.apiBroker.GetPodcastsAsync();
        }
    }
}
