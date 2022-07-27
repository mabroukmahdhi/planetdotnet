// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Foundations.GeoPositions;
using System;
using System.Collections.Generic;

namespace PlanetDotnet.Models.Foundations.Authors
{
    public class JamesMontemagno : IWorkAtMicrosoft
    {
        public string FirstName => "James";
        public string LastName => "Montemagno";
        public string StateOrRegion => "PNW";
        public string EmailAddress => "";
        public string ShortBioOrTagLine => "is a Principal Lead Program Manager for .NET Community";
        public Uri WebSite => new Uri("https://montemagno.com");

        public IEnumerable<Uri> FeedUris
        {
            get
            {
                return new Uri[]
                {
                    new Uri("https://montemagno.com/rss"),
                    new Uri("https://www.youtube.com/feeds/videos.xml?channel_id=UCENTmbKaTphpWV2R2evVz2A")
                };
            }
        }

        public string TwitterHandle => "JamesMontemagno";
        public string GravatarHash => "5df4d86308e585c879c19e5f909d8bfe";
        public string GitHubHandle => "jamesmontemagno";
        public GeoPosition Position => new GeoPosition(47.6541770, -122.3500000);
        public string FeedLanguageCode => "en";

    }

}
