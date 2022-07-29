// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Brokers.Gravatars;
using PlanetDotnet.Api.Brokers.Loggings;
using PlanetDotnet.Api.Services.Foundations.Authors;

[assembly: FunctionsStartup(typeof(PlanetDotnet.Api.Startup))]
namespace PlanetDotnet.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ILogger, Logger<LoggingBroker>>();
            builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();
            builder.Services.AddSingleton<IAuthorBroker, AuthorBroker>();
            builder.Services.AddScoped<IGravatarBroker, GravatarBroker>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
        }
    }

}
