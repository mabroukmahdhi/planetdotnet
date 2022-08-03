// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Podcasts;
using PlanetDotnet.Services.Views.Podcasts;
using PlanetDotnet.Shared.Abstractions;

namespace PlanetDotnet.Views.Components.PodcastComponents
{
    public partial class PodcastComponent : ComponentBase
    {
        [Inject]
        public IPodcastViewService PodcastViewService { get; set; }

        [Parameter]
        public IAmACommunityMember Member { get; set; }

        public PodcastView PodcastView { get; set; }

        protected override void OnParametersSet()
        {
            try
            {
                this.PodcastView = PodcastViewService
                     .InitializePodcastView(Member);
            }
            catch
            { }
        }
    }
}
