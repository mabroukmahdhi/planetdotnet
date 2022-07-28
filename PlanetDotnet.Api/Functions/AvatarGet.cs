using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PlanetDotnet.Api.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlanetDotnet.Api.Functions
{
    public static class AvatarGet
    {
        [FunctionName("AvatarGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "avatar")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("MD5 Hash function processed a request.");

            string email = req.Query["email"];

            if (string.IsNullOrWhiteSpace(email))
                return new BadRequestResult();

            string hash = new HashService().CreateMd5Hash(email);

            if (string.IsNullOrWhiteSpace(email))
                return new BadRequestResult();

            using HttpClient httpClient = new HttpClient();

            byte[] data = await httpClient.GetByteArrayAsync($"https://www.gravatar.com/avatar/{hash}.jpg?s=200&d=mm");

            return new FileContentResult(data, "image/jpg");
        }
    }
}
