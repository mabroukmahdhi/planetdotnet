// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Brokers.Gravatars;
using PlanetDotnet.Api.Brokers.Loggings;
using PlanetDotnet.Api.Models.Foundations.Authors.Exceptions;
using PlanetDotnet.Shared.Abstractions;
using System;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Services.Foundations.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorBroker authorBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IGravatarBroker gravatarBroker;

        public AuthorService(
            IAuthorBroker authorBroker,
            ILoggingBroker loggingBroker,
            IGravatarBroker gravatarBroker)
        {
            this.authorBroker = authorBroker;
            this.loggingBroker = loggingBroker;
            this.gravatarBroker = gravatarBroker;
        }

        public IEnumerable<IAmACommunityMember> RetrieveAllAuthors()
        {
            try
            {
                var authors = this.authorBroker.SelectAllAuthers();

                foreach (var author in authors)
                {
                    author.Avatar =
                        this.gravatarBroker.GetGravatarImage(
                            member: author);
                }

                return authors;
            }
            catch (Exception exception)
            {
                var authorServiceException =
                    new AuthorServiceException(exception);

                this.loggingBroker.LogError(authorServiceException);

                throw authorServiceException;
            }
        }
    }
}
