// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Services.Views.MapViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Views.Components.MapComponents
{
    public partial class MapComponent : ComponentBase
    {
        [Inject]
        public IMapViewService MapViewService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public IEnumerable<IAmACommunityMember> Authors { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var markers = MapViewService.CreateMarkers(Authors);

                await JSRuntime.InvokeVoidAsync("loadMap", markers);
            }
        }

    }
}
