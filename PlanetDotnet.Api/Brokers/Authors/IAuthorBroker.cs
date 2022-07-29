// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Models.Foundations.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Api.Brokers.Authors
{
    public interface IAuthorBroker
    {
        IEnumerable<IAmACommunityMember> SelectAllAuthers();
    }
}
