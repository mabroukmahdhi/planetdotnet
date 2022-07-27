// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Bases.Headers;
using SharpStyles.Models;

namespace PlanetDotnet.Views.Bases.Headers
{
    public partial class HeaderBase : ComponentBase
    {
        [Parameter]
        public RenderFragment Left { get; set; }

        [Parameter]
        public RenderFragment Right { get; set; }

        public SharpStyle Styles { get; set; }
        protected override void OnInitialized()
        {
            SetupStyles();
        }
        private void SetupStyles()
        {
            Styles = new HeaderStyles
            {
                NavbarCollapsed = new SharpStyle
                {
                    Overflow = "hidden",
                    MaxHeight = "none !important",
                    Height = "auto !important"
                }
            };
        }
    }
}
