// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Authors;
using PlanetDotnet.Brokers.Gravatars;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Models.Foundations.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Services.Foundations.Authors
{
    public partial class AuthorService : IAuthorService
    {
        private readonly IAuthorBroker authorBroker;
        private readonly IGravatarBroker gravatarBroker;
        private readonly ILoggingBroker loggingBroker;

        public AuthorService(
            IAuthorBroker authorBroker,
            IGravatarBroker gravatarBroker,
            ILoggingBroker loggingBroker)
        {
            this.authorBroker = authorBroker;
            this.gravatarBroker = gravatarBroker;
            this.loggingBroker = loggingBroker;
        }

        public IEnumerable<IAmACommunityMember> RetrieveAllAuthers()
        {
            return this.authorBroker.SelectAllAuthers();
        }

        public string RetrieveAuthorImage(IAmACommunityMember author) =>
        TryCatch(() =>
        {
            ValidateAuthor(author);

            return this.gravatarBroker.GetGravatarImage(author);
        });
    }

}
