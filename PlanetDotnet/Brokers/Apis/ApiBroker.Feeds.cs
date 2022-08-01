// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string GetFeedRelativeUrl = "api/rss?max=400&tag=.NET&lng=EN";

        public async ValueTask<string> GetFeedAsync() =>
             await this.httpClient.GetStringAsync(
               requestUri: GetFeedRelativeUrl);
    }
}
