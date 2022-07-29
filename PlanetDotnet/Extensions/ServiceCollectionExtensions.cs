// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlanetDotnet.Brokers.Apis;
using PlanetDotnet.Brokers.Localizations;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Brokers.Navigations;
using PlanetDotnet.Services.Foundations.Authors;
using PlanetDotnet.Services.Foundations.Localizations;
using PlanetDotnet.Services.Views.Authors.ListViews;
using PlanetDotnet.Services.Views.Authors.Profiles;
using PlanetDotnet.Services.Views.MapViews;
using PlanetDotnet.Services.Views.Podcasts;
using PlanetDotnet.Services.Views.Welcomes;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PlanetDotnet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBrokers(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger<LoggingBroker>>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddBlazoredLocalStorage();
            services.AddScoped<ILocalizationBroker, LocalizationBroker>();
            services.AddScoped<INavigationBroker, NavigationBroker>();
            services.AddScoped<IApiBroker, ApiBroker>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ILocalizatonService, LocalizatonService>();
            services.AddScoped<IProfileViewService, ProfileViewService>();
            services.AddScoped<IAuthorListViewService, AuthorListViewService>();
            services.AddScoped<IMapViewService, MapViewService>();
            services.AddScoped<IPodcastViewService, PodcastViewService>();
            services.AddScoped<IWelcomeViewService, WelcomeViewService>();
        }

        public static async ValueTask SetDefaultCulture(
            this IServiceProvider services)
        {
            var localizatonService =
                 services.GetRequiredService<ILocalizatonService>();

            var culture =
                await localizatonService.GetCurrentCultureAsnyc();

            if (string.IsNullOrWhiteSpace(culture))
            {
                culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                await localizatonService.SetCurrentCultureAsnyc(culture);
            }

            CultureInfo.DefaultThreadCurrentCulture =
                new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentUICulture =
                new CultureInfo(culture);
        }
    }

}
