// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Tags;
using PlanetDotnet.Shared.Abstractions.Tags;
using SharpStyles.Models;
using System.Collections.Generic;

namespace PlanetDotnet.Views.Components.TagComponents
{
    public partial class TagComponent : ComponentBase
    {
        [Parameter]
        public IEnumerable<Tag> Tags { get; set; }

        public SharpStyle Style { get; set; }

        protected override void OnInitialized()
        {
            SetupStyle();
        }

        private void SetupStyle()
        {
            Style = new TageStyles
            {
                TagSpan = new SharpStyle
                {
                    Display = "inline-block",
                    MarginLeft = "5px",
                    MarginRight = "5px",
                    FontSize = "13px",
                    FontFamily = "consolas"
                }
            };
        }
    }
}
