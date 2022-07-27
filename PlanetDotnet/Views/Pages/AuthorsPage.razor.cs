// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Services.Views.Authors.ListViews;
using System.Collections.Generic;
using System.Linq;

namespace PlanetDotnet.Views.Pages
{
    public partial class AuthorsPage : ComponentBase
    {
        [Inject]
        public IAuthorListViewService AuthorsViewService { get; set; }

        public IEnumerable<IAmACommunityMember> Members { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                this.Members = AuthorsViewService
                    .LoadCommunityMembers();
            }
            catch
            { }
        }

        private void SearchTextChanged(ChangeEventArgs args)
        {
            var authers = AuthorsViewService.LoadCommunityMembers();

            var name = args.Value?.ToString();

            if (string.IsNullOrWhiteSpace(name))
                this.Members = authers;

            this.Members = authers.Where(i =>
                $"{i.FirstName}{i.LastName}".ToLower()
                    .Contains(name));

            StateHasChanged();
        }
    }
}
