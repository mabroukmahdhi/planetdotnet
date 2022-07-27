// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PlanetDotnet.Api
{
    public static class HomeGet
    {
        [FunctionName("HomeGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "home")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult("Welcome to Planet .NET");
        }
    }
}
