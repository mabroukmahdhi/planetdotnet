﻿// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace PlanetDotnet.Api.Models.Apis.FeedRequests
{
    public class FeedRequest
    {
        public int MaxItems { get; set; } = 150;
        public string FeedLanguage { get; set; } = "mixed";
        public IEnumerable<AuthorInfo> Authors { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}