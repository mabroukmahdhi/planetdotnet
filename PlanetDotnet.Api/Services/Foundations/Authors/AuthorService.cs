// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Brokers.Loggings;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Services.Foundations.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorBroker authorBroker;
        private readonly ILoggingBroker loggingBroker;

        public AuthorService(
            IAuthorBroker authorBroker,
            ILoggingBroker loggingBroker)
        {
            this.authorBroker = authorBroker;
            this.loggingBroker = loggingBroker;
        }

        public IEnumerable<IAmACommunityMember> RetrieveAllAuthors() =>
            this.authorBroker.SelectAllAuthers();
    }
}
