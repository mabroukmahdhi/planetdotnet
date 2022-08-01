// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Apis;
using PlanetDotnet.Brokers.Loggings;
using System;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Feeds
{
    public class FeedService : IFeedService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public FeedService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<string> RetrieveFeedAsync()
        {
            try
            {
                return await this.apiBroker.GetFeedAsync();
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError(exception);

                return string.Empty;
            }
        }
    }
}
