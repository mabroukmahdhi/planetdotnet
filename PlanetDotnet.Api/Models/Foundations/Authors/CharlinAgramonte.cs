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
    public class CharlinAgramonte : IAmAMicrosoftMVP
    {
        public string FirstName => "Charlin";
        public string LastName => "Agramonte";
        public string ShortBioOrTagLine => "Software Engineer";
        public string StateOrRegion => "Dominican Republic";
        public string EmailAddress => "charlin@crossgeeks.com";
        public string TwitterHandle => "Chard003";
        public string GravatarHash => "7db2bb2eed17e8df7e78b0d5461d90b0";
        public string GitHubHandle => "char0394";
        public GeoPosition Position => new GeoPosition(18.4735438, -69.9456919);

        public Uri WebSite => new Uri("https://xamgirl.com/");
        public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://xamgirl.com/rss"); } }
        public string FeedLanguageCode => "en";
        public IEnumerable<Tag> Tags => new List<Tag> { Tag.Xamarin, Tag.DotNetMAUI };
    }

}
