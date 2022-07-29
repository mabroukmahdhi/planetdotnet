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
using PlanetDotnet.Brokers.Authors;
using System.Threading.Tasks;

namespace PlanetDotnet.Api.Functions
{
    public class AuthorsGet
    {
        private readonly IAuthorBroker authorBroker;

        public AuthorsGet(IAuthorBroker authorBroker) =>
            this.authorBroker = authorBroker;


        [FunctionName("AuthorsGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "authors")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Authors processed a request.");

            var authors = this.authorBroker.SelectAllAuthers();

            return new OkObjectResult(authors);
        }
    }
}
