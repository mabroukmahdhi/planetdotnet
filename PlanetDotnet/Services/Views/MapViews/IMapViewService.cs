// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Markers;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Services.Views.MapViews
{
    public interface IMapViewService
    {
        IEnumerable<Marker> CreateMarkers(IEnumerable<IAmACommunityMember> authors);
    }
}
