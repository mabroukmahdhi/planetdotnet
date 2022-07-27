// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace PlanetDotnet.Models.Views.Bases.Headers
{
    public class HeaderStyles : SharpStyle
    {
        [CssClass(selector = ".navbar-collapse.in")]
        public SharpStyle NavbarCollapsed { get; set; }
    }
}
