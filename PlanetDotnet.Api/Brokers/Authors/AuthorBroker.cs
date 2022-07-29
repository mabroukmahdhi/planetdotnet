// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Brokers.Authors
{
    public partial class AuthorBroker : IAuthorBroker
    {
        private readonly IEnumerable<IAmACommunityMember> members;

        public AuthorBroker(IEnumerable<IAmACommunityMember> members) =>
            this.members = members;

        public IEnumerable<IAmACommunityMember> SelectAllAuthers()
        {
            return members;
        }
    }
}
