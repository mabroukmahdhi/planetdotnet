// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Apis.FeedRequests;
using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask PostFeedAsync(FeedRequest feedRequest);
        ValueTask<string> GetFeedAsync();
    }
}
