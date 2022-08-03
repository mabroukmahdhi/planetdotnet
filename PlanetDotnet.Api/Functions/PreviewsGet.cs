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
using System;
using System.Threading.Tasks;

namespace PlanetDotnet.Api.Functions
{
    public class PreviewsGet
    {
        private readonly IAuthorService authorService;
        public PreviewsGet(IAuthorService authorService) =>
            this.authorService = authorService;

        [FunctionName("PreviewsGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "previews")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var previews = await this.authorService.RetrieveAllPreviewsAsync();

                return new OkObjectResult(previews);
            }
            catch (AuthorServiceException authorServiceException)
            {
                var message =
                    authorServiceException.InnerException?.Message;

                log.LogError(message);

                return new ConflictObjectResult(message);
            }
            catch (Exception exception)
            {
                log.LogError(exception.Message);

                return new BadRequestResult();
            }
        }
    }
}
