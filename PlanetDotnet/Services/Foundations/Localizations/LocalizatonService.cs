// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Blazored.LocalStorage;
using PlanetDotnet.Brokers.Localizations;
using PlanetDotnet.Brokers.Navigations;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Localizations
{
    public class LocalizatonService : ILocalizatonService
    {
        private readonly ILocalizationBroker localizationBroker;
        private readonly INavigationBroker navigationBroker;
        private readonly ILocalStorageService localStorageService;

        private const string ArabicKey = "ar";
        private const string CurrentCultureKey = "CurrentCulture";

        public LocalizatonService(
            ILocalizationBroker localizationBroker,
            INavigationBroker navigationBroker,
            ILocalStorageService localStorageService)
        {
            this.localizationBroker = localizationBroker;
            this.navigationBroker = navigationBroker;
            this.localStorageService = localStorageService;
        }

        public string this[string key] =>
            this.localizationBroker.GetText(key);

        public bool IsRightToLeft =>
           CultureInfo.CurrentCulture
           .TwoLetterISOLanguageName
           .Equals(
               ArabicKey,
               StringComparison.OrdinalIgnoreCase);

        public async ValueTask SetCurrentCultureAsnyc(
            string culture,
            bool reloadPage = false)
        {
            await localStorageService.RemoveItemAsync(
                CurrentCultureKey);

            await this.localStorageService.SetItemAsync(
                key: CurrentCultureKey,
                data: culture);

            if (reloadPage)
            {
                this.navigationBroker.BackToHome();
            }
        }

        public async ValueTask<string> GetCurrentCultureAsnyc() =>
            await this.localStorageService.GetItemAsync<string>(CurrentCultureKey);

        public async ValueTask DeleteCurrentCultureAsnyc() =>
            await localStorageService.RemoveItemAsync(CurrentCultureKey);
    }

}
