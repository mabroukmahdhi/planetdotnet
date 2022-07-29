// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Services.Foundations.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorBroker authorBroker;

        public AuthorService(IAuthorBroker authorBroker) =>
             this.authorBroker = authorBroker;

        public IEnumerable<IAmACommunityMember> RetrieveAllAuthors() =>
            this.authorBroker.SelectAllAuthers();
    }
}
