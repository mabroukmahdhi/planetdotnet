// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace PlanetDotnet.Api.Models.Foundations.GeoPositions
{
    public class GeoPosition
    {
        public static GeoPosition Empty =
            new GeoPosition(-1337, 42);

        public double Lat { get; }
        public double Lng { get; }

        public GeoPosition(
            double lat,
            double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }
}
