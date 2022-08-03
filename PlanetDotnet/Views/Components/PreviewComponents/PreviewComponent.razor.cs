// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Previews;

namespace PlanetDotnet.Views.Components.PreviewComponents
{
    public partial class PreviewComponent : ComponentBase
    {
        [Parameter]
        public PreviewItemView Preview { get; set; }
    }
}
