// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Previews;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string GetPreviewsRelativeUrl = "api/previews";

        public async ValueTask<IEnumerable<PreviewItem>> GetPreviewsAsync() =>
             await this.httpClient.GetFromJsonAsync<IEnumerable<PreviewItem>>(
                 requestUri: GetPreviewsRelativeUrl);
    }
}
