// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Authors;
using PlanetDotnet.Services.Views.Authors.Profiles;
using PlanetDotnet.Shared.Abstractions;

namespace PlanetDotnet.Views.Components.ProfileComponents
{
    public partial class ProfileComponent : ComponentBase
    {
        [Inject]
        public IProfileViewService ProfileViewService { get; set; }

        [Parameter]
        public IAmACommunityMember Member { get; set; }

        public ProfileView ProfileView { get; set; }

        protected override void OnParametersSet()
        {
            try
            {
                this.ProfileView = ProfileViewService
                     .InitializeProfileView(Member);
            }
            catch
            { }
        }

    }

}
