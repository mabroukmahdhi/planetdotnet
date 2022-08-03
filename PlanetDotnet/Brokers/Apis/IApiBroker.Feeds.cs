// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace PlanetDotnet.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<string> GetFeedAsync();
    }
}
