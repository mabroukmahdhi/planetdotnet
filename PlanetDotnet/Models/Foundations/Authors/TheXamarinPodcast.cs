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
    public class TheXamarinPodcast : IAmACommunityMember, IAmAPodcast
    {
        public string FirstName => "The .NET MAUI";
        public string LastName => "Podcast";
        public string StateOrRegion => "Internet";
        public string EmailAddress => "hello@xamarin.com";
        public string ShortBioOrTagLine => "is the official podcast of .NET MAUI!";
        public Uri WebSite => new Uri("http://www.xamarinpodcast.com");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://feeds.fireside.fm/xamarinpodcast/rss"); }
        }

        public string TwitterHandle => "XamarinPodcast";
        public string GravatarHash => "e7d599acf639d9d3883a0fd477b3ba42";
        public string GitHubHandle => string.Empty;
        public GeoPosition Position => GeoPosition.Empty;
        public string FeedLanguageCode => "en";

        public IEnumerable<Tag> Tags => new List<Tag> { Tag.Xamarin, Tag.DotNetMAUI };
    }

}
