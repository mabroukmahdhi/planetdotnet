// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using PlanetDotnet.Brokers.Authors;
using PlanetDotnet.Brokers.Feeds;
using PlanetDotnet.Brokers.Navigations;
using PlanetDotnet.Models.Foundations.Configurations;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Feeds
{
    public class FeedService : IFeedService
    {
        private readonly IFeedBroker feedBroker;
        private readonly IAuthorBroker authorBroker;
        private readonly INavigationBroker navigationBroker;
        private readonly LocalConfigurations localConfigurations;

        public FeedService(
            IFeedBroker feedBroker,
            IAuthorBroker authorBroker,
            INavigationBroker navigationBroker,
            IConfiguration configuration)
        {
            this.feedBroker = feedBroker;
            this.authorBroker = authorBroker;
            this.navigationBroker = navigationBroker;

            this.localConfigurations =
                configuration.Get<LocalConfigurations>();
        }

        public async ValueTask InitializeFeedsAsync()
        {
            var authors = this.authorBroker.SelectAllAuthers();

          //  await this.feedBroker.CeateFeedFileAsync(authors);
        }

        public void NavigateToFeeds() =>
            this.navigationBroker.NavigateTo(
                url: this.localConfigurations.FeedPagePath);
    }
}
