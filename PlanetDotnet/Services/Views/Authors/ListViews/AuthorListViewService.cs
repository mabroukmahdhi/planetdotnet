// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Services.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Views.Authors.ListViews
{
    public class AuthorListViewService : IAuthorListViewService
    {
        private readonly IAuthorService authorService;

        public AuthorListViewService(IAuthorService authorService) =>
             this.authorService = authorService;

        public async ValueTask<IEnumerable<IAmACommunityMember>> LoadAuthorsAsync() =>
            await this.authorService.RetrieveAllAuthorsAsync();
    }
}
