// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Authors
{
    public interface IAuthorService
    {
        IEnumerable<IAmACommunityMember> RetrieveAllAuthers();

        string RetrieveAuthorImage(IAmACommunityMember author);
        void PostFeeds();
    }
}
