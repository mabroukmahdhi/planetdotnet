// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using System.Collections.Generic;

namespace PlanetDotnet.Services.Views.Authors.ListViews
{
    public interface IAuthorListViewService
    {
        IEnumerable<IAmACommunityMember> LoadCommunityMembers();
    }
}
