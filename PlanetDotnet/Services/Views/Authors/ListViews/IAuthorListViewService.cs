// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Views.Authors.ListViews
{
    public interface IAuthorListViewService
    {
        ValueTask<IEnumerable<IAmACommunityMember>> LoadAuthorsAsync();
    }
}
