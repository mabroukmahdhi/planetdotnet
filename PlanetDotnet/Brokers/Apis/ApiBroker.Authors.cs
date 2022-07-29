// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string GetAuthorsRelativeUrl = "api/authors";

        public async ValueTask<IEnumerable<IAmACommunityMember>> GetAuthorsAsync()
        {
            return await this.httpClient.GetFromJsonAsync<IEnumerable<Author>>(
                requestUri: GetAuthorsRelativeUrl);
        }
    }
}
