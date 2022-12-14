// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;

namespace PlanetDotnet.Api.Brokers.Gravatars
{
    public interface IGravatarBroker
    {
        string GetGravatarImage(IAmACommunityMember member);
    }
}
