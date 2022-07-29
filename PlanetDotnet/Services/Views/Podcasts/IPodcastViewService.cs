// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Views.Podcasts;
using PlanetDotnet.Shared.Abstractions;

namespace PlanetDotnet.Services.Views.Podcasts
{
    public interface IPodcastViewService
    {
        PodcastView InitializePodcastView(IAmACommunityMember author);
    }
}
