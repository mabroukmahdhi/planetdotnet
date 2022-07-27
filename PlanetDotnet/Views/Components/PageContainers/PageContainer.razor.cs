// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace PlanetDotnet.Views.Components.PageContainers
{
    public partial class PageContainer : ComponentBase
    {
        [Parameter]
        public RenderFragment Body { get; set; }

        public string Direction => Localizer.IsRightToLeft ? "rtl" : "ltr";
    }
}
