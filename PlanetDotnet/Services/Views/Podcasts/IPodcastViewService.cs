// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Views.Podcasts;

namespace PlanetDotnet.Services.Views.Podcasts
{
    public interface IPodcastViewService
    {
        PodcastView InitializePodcastView(IAmACommunityMember author);
    }
}
