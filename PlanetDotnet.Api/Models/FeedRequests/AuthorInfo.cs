﻿// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace PlanetDotnet.Api.Models.FeedRequests
{
    public class AuthorInfo
    {
        public string FullName { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public IEnumerable<string> FeedUris { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}