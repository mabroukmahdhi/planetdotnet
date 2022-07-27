// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Autofac;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System;
using Autofac.Extensions.DependencyInjection;

namespace PlanetDotnet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBrokers(this IServiceCollection services)
        {
            
        }

        public static void AddServices(this IServiceCollection services)
        {
            
        }

        public static void AddAutofacServiceProvider(this WebAssemblyHostBuilder builder)
        {
            builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyModules(assembly);
        }

    }

}
