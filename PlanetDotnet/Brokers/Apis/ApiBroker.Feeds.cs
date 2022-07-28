// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Apis.FeedRequests;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string PostFeedRelativeUrl = "api/feed";
        private const string GetFeedRelativeUrl = "api/feed";

        public async ValueTask<string> GetFeedAsync() =>
             await this.httpClient.GetStringAsync(
               requestUri: GetFeedRelativeUrl);

        public async ValueTask PostFeedAsync(FeedRequest feedRequest)
        {
            await this.httpClient.PostAsJsonAsync(
               requestUri: PostFeedRelativeUrl, feedRequest);
        }
    }
}
