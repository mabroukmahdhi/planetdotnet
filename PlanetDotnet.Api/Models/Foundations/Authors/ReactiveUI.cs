// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Models.Foundations.Abstractions;
using PlanetDotnet.Api.Models.Foundations.GeoPositions;
using PlanetDotnet.Api.Models.Foundations.Tags;
using System;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Models.Foundations.Authors
{
    public class ReactiveUI : IAmAFrameworkForDotNet
    {
        public string FirstName => "ReactiveUI";
        public string LastName => "";
        public string StateOrRegion => "Internet";
        public string EmailAddress => "hello@reactiveui.net";
        public string ShortBioOrTagLine => "An advanced, composable, functional reactive model-view-viewmodel framework for all .NET platforms";
        public Uri WebSite => new Uri("https://reactiveui.net/");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://reactiveui.net/rss"); }
        }

        public string TwitterHandle => "ReactiveXUI";
        public string GravatarHash => "";
        public string GitHubHandle => "ReactiveUI";
        public GeoPosition Position => new GeoPosition(-13.6981464, 37.3979239);
        public string FeedLanguageCode => "en";
        public IEnumerable<Tag> Tags => new List<Tag> { Tag.Default };
    }

}
