// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Foundations.GeoPositions;
using System.Collections.Generic;
using System;
using PlanetDotnet.Models.Foundations.Tags;

namespace PlanetDotnet.Models.Foundations.Authors
{
    public class TheXamarinShow : IAmACommunityMember, IAmAPodcast
    {
        public string FirstName => "The Xamarin";
        public string LastName => "Show";
        public string StateOrRegion => "Channel 9";
        public string EmailAddress => "";
        public string ShortBioOrTagLine => "is a Weekly Developer Show for Xamarin Developers";
        public Uri WebSite => new Uri("http://xamarinshow.com");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://channel9.msdn.com/Shows/XamarinShow/feed"); }
        }

        public string TwitterHandle => "TheXamarinShow";
        public string GravatarHash => "7a0c7da0279b4e90439e780fa01924e0";
        public string GitHubHandle => string.Empty;
        public GeoPosition Position => new GeoPosition(47.6451360, -122.1309390);
        public string FeedLanguageCode => "en";
        public IEnumerable<Tag> Tags => new List<Tag> { Tag.Xamarin };
    }
}
