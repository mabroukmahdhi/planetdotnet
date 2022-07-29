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
using PlanetDotnet.Api.Models.Foundations.Authors.Exceptions;
using PlanetDotnet.Api.Services.Foundations.Authors;

namespace PlanetDotnet.Api.Functions
{

    public class AuthorsGet
    {
        private readonly IAuthorService authorService;
        public AuthorsGet(IAuthorService authorService) =>
            this.authorService = authorService;

        [FunctionName("AuthorsGet")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "authors")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var authors = this.authorService.RetrieveAllAuthors();

                return new OkObjectResult(authors);
            }
            catch (AuthorServiceException authorServiceException)
            {
                return new ConflictObjectResult(
                    authorServiceException.InnerException?.Message);
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}
