// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using PlanetDotnet.Models.Views.Welcomes;
using PlanetDotnet.Services.Views.Welcomes;
using System.Threading.Tasks;

namespace PlanetDotnet.Views.Components.WelcomeComponents
{
    public partial class WelcomeComponent : ComponentBase
    {
        [Inject]
        public IWelcomeViewService WelcomeViewService { get; set; }

        public WelcomeView WelcomeView { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.WelcomeView = await WelcomeViewService
                     .InitializeWelcomeViewAsync();
        }
    }
}
