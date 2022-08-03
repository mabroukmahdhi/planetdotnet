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
    public class MapViewService : IMapViewService
    {
        public IEnumerable<Marker> CreateMarkers(
            IEnumerable<IAmACommunityMember> authors)
        {
            if (authors == null)
                yield return default;

            foreach (var member in authors)
            {
                yield return new Marker
                {
                    Id = $"{member.FirstName}{member.LastName}",
                    Lat = member.Position.Lat,
                    Lng = member.Position.Lng,
                    Name = $"{member.FirstName} {member.LastName}",
                    Gravatar = member.GravatarHash,
                };
            }
        }
    }
}
