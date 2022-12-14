// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace PlanetDotnet.Models.Views.Authors
{
    public class ProfileView
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string TwitterHandle { get; set; }
        public string Description { get; set; }
        public string StateOrRegion { get; set; }
        public string WebSite { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
