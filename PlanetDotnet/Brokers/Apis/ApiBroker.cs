// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Net.Http;

namespace PlanetDotnet.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private readonly HttpClient httpClient;

        public ApiBroker(HttpClient httpClient) =>
             this.httpClient = httpClient;
    }
}
