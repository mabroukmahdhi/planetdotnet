using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlanetDotnet.Api.Models.FeedRequests;
using PlanetDotnet.Api.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlanetDotnet.Api
{
    public static class FeedPost
    {
        [FunctionName("FeedPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "feed")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("LoadFeeds function processed a request.");

                using StreamReader streamReader = new(req.Body);
                var requestBody = await streamReader.ReadToEndAsync();

                var feedRequest = JsonConvert.DeserializeObject<FeedRequest>(requestBody);

                var feedService = new FeedService();

                string xmlFeed = await feedService.CreateAndLoadFeedAsync(feedRequest);

                return new OkObjectResult(xmlFeed);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "LoadFeeds function error occurs");

                return new BadRequestResult();
            }
        }
    }
}
