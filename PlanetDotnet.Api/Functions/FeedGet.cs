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
    public class FeedGet
    {
        private readonly IAuthorService authorService;
        public FeedGet(IAuthorService authorService) =>
            this.authorService = authorService;

        [FunctionName("FeedGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rss")] HttpRequest req,
            ILogger log)
        {
            try
            {
                Int32.TryParse(req.Query["max"], out var max);
                if (max == 0)
                {
                    max = 400;
                }

                var tag = req.Query["tag"].ToString().ToLower();
                var lng = req.Query["lng"].ToString().ToLower();

                var xmlFeed = await this.authorService.RetrieveXmlFeedAsync(
                    numberOfItems: max,
                    tag: tag,
                    languageCode: lng);

                return new OkObjectResult(xmlFeed);
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
