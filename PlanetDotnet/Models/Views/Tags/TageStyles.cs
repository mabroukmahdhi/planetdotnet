// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace PlanetDotnet.Models.Views.Tags
{
    public class TageStyles : SharpStyle
    {
        [CssClass]
        public SharpStyle TagSpan { get; set; }
    }
}
