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
using PlanetDotnet.Shared.Abstractions;
using System;
using System.Linq;

namespace PlanetDotnet.Api.Functions
{
    public class PodcastsGet
    {
        private readonly IAuthorService authorService;
        public PodcastsGet(IAuthorService authorService) =>
            this.authorService = authorService;

        [FunctionName("PodcastsGet")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "podcasts")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var authors = this.authorService.RetrieveAllAuthors();

                var podcasts = authors?.Where(author =>
                    author is IAmAPodcast
                    || author is IAmANewsletter
                    || author is IAmAFrameworkForDotNet);

                return new OkObjectResult(podcasts);
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
