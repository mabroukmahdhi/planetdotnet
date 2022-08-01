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
                int max = 400;
                var paresed = req.Query.TryGetValue("max", out var maxItems);
                if (paresed)
                {
                    paresed = int.TryParse(maxItems, out max);
                }

                req.Query.TryGetValue("tag", out var tag);
                req.Query.TryGetValue("lng", out var lng);

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
