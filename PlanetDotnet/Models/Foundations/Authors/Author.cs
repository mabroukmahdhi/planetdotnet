// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;
using PlanetDotnet.Shared.Abstractions.GeoPositions;
using System;
using System.Collections.Generic;

namespace PlanetDotnet.Models.Foundations.Authors
{
    public class Author : IAmACommunityMember
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StateOrRegion { get; set; }

        public string EmailAddress { get; set; }

        public string ShortBioOrTagLine { get; set; }

        public Uri WebSite { get; set; }

        public string TwitterHandle { get; set; }

        public string GitHubHandle { get; set; }

        public string GravatarHash { get; set; }

        public string Avatar { get; set; }

        public IEnumerable<Uri> FeedUris { get; set; }

        public GeoPosition Position { get; set; }

        public string FeedLanguageCode { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
