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
using System.Text;
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
                var tag = "all";
                var lng = "mixed";

                var xmlFeed = await this.authorService.RetrieveXmlFeedAsync(
                    numberOfItems: max,
                    tag: tag,
                    languageCode: lng);

                byte[] filebytes = Encoding.UTF8.GetBytes(xmlFeed);

                return new FileContentResult(filebytes, "application/rss+xml");
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
