// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Feeds
{
    public interface IFeedBroker
    {
        ValueTask CeateFeedFileAsync(IEnumerable<IAmACommunityMember> authors);
    }
}
