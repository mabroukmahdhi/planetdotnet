// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Brokers.Authors
{
    public interface IAuthorBroker
    {
        IEnumerable<IAmACommunityMember> SelectAllAuthers();
    }
}
