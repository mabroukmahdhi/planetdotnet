// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.ServiceModel.Syndication;

namespace PlanetDotnet.Shared.Abstractions
{
    public interface IFilterMyBlogPosts
    {
        bool Filter(SyndicationItem item);
    }
}
