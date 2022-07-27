// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Services.Foundations.Authors;
using System.Collections.Generic;

namespace PlanetDotnet.Services.Views.Authors.ListViews
{
    public class AuthorListViewService : IAuthorListViewService
    {
        private readonly IAuthorService authorService;

        public AuthorListViewService(IAuthorService authorService) =>
             this.authorService = authorService;

        public IEnumerable<IAmACommunityMember> LoadCommunityMembers() =>
            this.authorService.RetrieveAllAuthers();
    }
}
