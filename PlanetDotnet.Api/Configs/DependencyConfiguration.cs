// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Autofac;
using AzureFunctions.Autofac.Configuration;
using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Brokers.Gravatars;
using PlanetDotnet.Api.Brokers.Loggings;

namespace PlanetDotnet.Api.Configs
{
    public class DependencyConfiguration
    {
        public DependencyConfiguration(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterAssemblyModules(typeof(AuthorBroker).Assembly);
                builder.RegisterType<AuthorBroker>().As<IAuthorBroker>();
                builder.RegisterType<LoggingBroker>().As<ILoggingBroker>();
                builder.RegisterType<GravatarBroker>().As<IGravatarBroker>();

            }, functionName);
        }
    }
}
