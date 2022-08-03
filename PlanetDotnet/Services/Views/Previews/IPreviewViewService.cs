// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Views.Previews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Views.Previews
{
    public interface IPreviewViewService
    {
        ValueTask<IEnumerable<PreviewItemView>> LoadPreviewsAsync();
    }
}
