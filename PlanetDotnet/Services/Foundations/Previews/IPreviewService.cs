// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Previews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Previews
{
    public interface IPreviewService
    {
        ValueTask<IEnumerable<PreviewItem>> RetrievePreviewsAsync();
    }
}
