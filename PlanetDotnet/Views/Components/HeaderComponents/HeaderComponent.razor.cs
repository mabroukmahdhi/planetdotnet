// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Services.Foundations.Feeds;

namespace PlanetDotnet.Views.Components.HeaderComponents
{
    public partial class HeaderComponent : ComponentBase
    {
        [Inject]
        public IFeedService FeedService { get; set; }

        private void FeedClicked()
        {
            this.FeedService.NavigateToFeeds();
        }
    }
}
