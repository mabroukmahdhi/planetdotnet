// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;
using PlanetDotnet.Shared.Abstractions.GeoPositions;
using PlanetDotnet.Shared.Abstractions.Tags;
using System;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Models.Foundations.Authors
{
    public class MergeConflict : IAmACommunityMember, IAmAPodcast
    {
        public string FirstName => "Merge";
        public string LastName => "Conflict";
        public string StateOrRegion => "Seattle, WA";
        public string EmailAddress => "mergeconflictfm@gmail.com";
        public string ShortBioOrTagLine => "is a weekly development podcast hosted by Frank Krueger and James Montemagno.";
        public Uri WebSite => new Uri("http://mergeconflict.fm");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://feeds.fireside.fm/mergeconflict/rss"); }
        }

        public string TwitterHandle => "MergeConflictFM";
        public string GravatarHash => "24527eb9b29a8adbfc4155db4044dd3c";
        public string GitHubHandle => string.Empty;
        public GeoPosition Position => new GeoPosition(47.6062100, -122.3320710);
        public string FeedLanguageCode => "en";
        public IEnumerable<string> Tags => new List<string> { Tag.Default };
        public string Avatar { get; set; }
    }

}
