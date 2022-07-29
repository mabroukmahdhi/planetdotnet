// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PlanetDotnet.Brokers.Authors;

[assembly: FunctionsStartup(typeof(PlanetDotnet.Api.Startup))]
namespace PlanetDotnet.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IAuthorBroker, AuthorBroker>();
        }
    }
}
