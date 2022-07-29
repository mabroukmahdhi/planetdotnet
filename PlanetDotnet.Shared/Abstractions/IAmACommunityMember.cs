// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions.GeoPositions;
using PlanetDotnet.Shared.Abstractions.Tags;

namespace PlanetDotnet.Shared.Abstractions
{
    public interface IAmACommunityMember
    {
        string FirstName { get; }
        string LastName { get; }
        string StateOrRegion { get; }
        string EmailAddress { get; }
        string ShortBioOrTagLine { get; }
        Uri WebSite { get; }
        string TwitterHandle { get; }
        string GitHubHandle { get; }
        string GravatarHash { get; }
        IEnumerable<Uri> FeedUris { get; }
        GeoPosition Position { get; }
        string FeedLanguageCode { get; }
        IEnumerable<Tag> Tags { get; }
    }
}
