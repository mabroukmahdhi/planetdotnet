// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Models.Views.Welcomes
{
    public class WelcomeView
    {
        public string TwitterLink { get; set; }
        public string FacebookLink { get; set; }
        public string FeedLink { get; set; }
        public string PreviewPagePath { get; set; }
        public string AuthorsPagePath { get; set; }
        public string GithubRepository { get; set; }
        public string BadgeImagePath { get; set; }
        public IEnumerable<IAmACommunityMember> Podcasts { get; set; }
    }
}
