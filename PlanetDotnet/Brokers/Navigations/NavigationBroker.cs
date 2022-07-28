// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace PlanetDotnet.Brokers.Navigations
{
    public class NavigationBroker : INavigationBroker
    {
        private readonly NavigationManager navigationManager;

        public NavigationBroker(NavigationManager navigationManager) =>
            this.navigationManager = navigationManager;

        public void BackToHome() =>
            this.navigationManager.NavigateTo(
                uri: "/",
                forceLoad: true);

        public void NavigateTo(string url) =>
            this.navigationManager.NavigateTo(url);
    }
}
