// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using AzureFunctions.Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Configs;
using System.Linq;

namespace PlanetDotnet.Api.Functions
{
    [DependencyInjectionConfig(typeof(DependencyConfiguration))]
    public static class AuthorsGet
    {

        [FunctionName("AuthorsGet")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "authors")] HttpRequest req,
            [Inject] IAuthorBroker authorBroker,
            ILogger log)
        {
            log.LogInformation("Authors processed a request.");

            var authors = authorBroker.SelectAllAuthers();

            log.LogInformation($"{authors?.Count()} author(s) found.");

            return new OkObjectResult(authors);
        }
    }
}
