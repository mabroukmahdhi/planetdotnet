// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlanetDotnet.Extensions;
using PlanetDotnet.Views;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddAutofacServiceProvider();

builder.Services.AddBrokers();
builder.Services.AddServices();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(
            builder.Configuration["API_Prefix"]
            ?? builder.HostEnvironment.BaseAddress)
    });

await builder.Build().RunAsync();
