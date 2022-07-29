// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Services.Views.Authors.ListViews;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetDotnet.Views.Pages
{
    public partial class AuthorsPage : ComponentBase
    {
        [Inject]
        public IAuthorListViewService AuthorsViewService { get; set; }

        public IEnumerable<IAmACommunityMember> Members { get; set; }
        private IEnumerable<IAmACommunityMember> source;

        protected override async Task OnInitializedAsync()
        {
            this.Members = this.source
                = await AuthorsViewService
                    .LoadAuthorsAsync();
        }

        private void SearchTextChanged(ChangeEventArgs args)
        {
            var authers = this.source;

            var name = args.Value?.ToString();

            if (string.IsNullOrWhiteSpace(name))
                this.Members = authers;

            this.Members = authers.Where(i =>
                $"{i.FirstName}{i.LastName}{string.Join("", i.Tags)}"
                .ToLowerInvariant()
                .Contains(name));

            StateHasChanged();
        }
    }
}
