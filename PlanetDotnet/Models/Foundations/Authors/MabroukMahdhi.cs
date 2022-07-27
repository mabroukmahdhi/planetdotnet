﻿// ---------------------------------------------------------------
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
    public class MabroukMahdhi : IAmACommunityMember
    {
        public string FirstName => "Mabrouk";

        public string LastName => "Mahdhi";

        public string StateOrRegion => "Germany";

        public string EmailAddress => "mabrouk@mahdhi.com";

        public string ShortBioOrTagLine => "is a Software-engineer and loves .NET";

        public Uri WebSite => new("https://mahdhi.com");

        public string TwitterHandle => "Mabrouk_Mahdhi";

        public string GitHubHandle => "mabroukmahdhi";

        public string GravatarHash => "1f5b179abb9b9f8a34a4a9799e205c96";

        public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://www.xamabrouk.com/feed"); } }

        public GeoPosition Position => new(50.297, 8.26896);

        public string FeedLanguageCode => "en";
    }
}