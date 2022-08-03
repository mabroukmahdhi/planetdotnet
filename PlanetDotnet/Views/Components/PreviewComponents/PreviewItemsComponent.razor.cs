// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Previews;
using PlanetDotnet.Services.Views.Previews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Views.Components.PreviewComponents
{
    public partial class PreviewItemsComponent : ComponentBase
    {
        [Inject]
        public IPreviewViewService PreviewViewService { get; set; }

        public IEnumerable<PreviewItemView> PreviewItems { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            PreviewItems =
                await PreviewViewService.LoadPreviewsAsync();
        }
    }
}
