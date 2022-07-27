// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace PlanetDotnet.Views.Bases.Styles
{
    public partial class StyleBase : ComponentBase
    {
        [Parameter]
        public SharpStyle Style { get; set; }
    }
}
