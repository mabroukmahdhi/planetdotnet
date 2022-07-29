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
using Newtonsoft.Json;
using PlanetDotnet.Api.Models.Apis.FeedRequests;
using PlanetDotnet.Api.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlanetDotnet.Api.Functions
{
    public static class FeedPost
    {
        [FunctionName("FeedPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "feed")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("LoadFeeds function processed a request.");

                var filePath = Path.Combine(Path.GetTempPath(), "feed.xml");

                string xmlFeed = string.Empty;

                if (req.Method == HttpMethods.Get)
                {
                    if (!File.Exists(filePath))
                        return new NotFoundResult();

                    return new PhysicalFileResult(filePath, "application/rss+xml");
                }

                using StreamReader streamReader = new(req.Body);
                var requestBody = await streamReader.ReadToEndAsync();

                var feedRequest = JsonConvert.DeserializeObject<FeedRequest>(requestBody);

                var feedService = new FeedService();

                xmlFeed = await feedService.CreateAndLoadFeedAsync(feedRequest);

                File.WriteAllText(filePath, xmlFeed);

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "LoadFeeds function error occurs");

                return new BadRequestResult();
            }
        }
    }
}
