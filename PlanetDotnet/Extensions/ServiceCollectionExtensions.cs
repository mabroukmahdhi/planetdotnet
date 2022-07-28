// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlanetDotnet.Brokers.Authors;
using PlanetDotnet.Brokers.Gravatars;
using PlanetDotnet.Brokers.Localizations;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Services.Foundations.Authors;
using PlanetDotnet.Services.Foundations.Localizations;
using PlanetDotnet.Services.Views.Authors.ListViews;
using PlanetDotnet.Services.Views.Authors.Profiles;
using PlanetDotnet.Services.Views.MapViews;
using PlanetDotnet.Services.Views.Podcasts;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace PlanetDotnet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBrokers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorBroker, AuthorBroker>();
            services.AddScoped<IGravatarBroker, GravatarBroker>();
            services.AddScoped<ILogger, Logger<LoggingBroker>>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddBlazoredLocalStorage();
            services.AddScoped<ILocalizationBroker, LocalizationBroker>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ILocalizatonService, LocalizatonService>();
            services.AddScoped<IProfileViewService, ProfileViewService>();
            services.AddScoped<IAuthorListViewService, AuthorListViewService>();
            services.AddScoped<IMapViewService, MapViewService>();
            services.AddScoped<IPodcastViewService, PodcastViewService>();
        }

        public static void AddAutofacServiceProvider(this WebAssemblyHostBuilder builder)
        {
            builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));
        }

        public static async Task SetDefaultCulture(
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


        private static void ConfigureContainer(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyModules(assembly);
        }

    }

}
