// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlanetDotnet.Api.Brokers.Authors
{
    public partial class AuthorBroker : IAuthorBroker
    {
        private readonly IEnumerable<IAmACommunityMember> members;

        public AuthorBroker()
        {
            this.members = GetAuthors();
        }

        public IEnumerable<IAmACommunityMember> SelectAllAuthers()
        {
            return this.members;
        }

        private static IEnumerable<IAmACommunityMember> GetAuthors()
        {
            var assembly = Assembly.GetAssembly(typeof(AuthorBroker));

            var types = assembly.GetTypes();

            var authorTypes = types.Where(type =>
                typeof(IAmACommunityMember).IsAssignableFrom(type)
                && !GetInterfacesNames().Contains(type.Name));

            foreach (var authorType in authorTypes)
            {
                var author = (IAmACommunityMember)Activator.CreateInstance(authorType);
                yield return author;
            }
        }

        private static string[] GetInterfacesNames() => new[]
        {
            nameof(IAmACommunityMember),
            nameof(IAmAFrameworkForDotNet),
            nameof(IAmAMicrosoftMVP),
            nameof(IAmANewsletter),
            nameof(IAmAPodcast),
            nameof(IAmAYoutuber),
            nameof(IWorkAtMicrosoft),
        };
    }
}
