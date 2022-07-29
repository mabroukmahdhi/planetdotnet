// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Views.Authors;
using PlanetDotnet.Shared.Abstractions;
using System;

namespace PlanetDotnet.Services.Views.Authors.Profiles
{
    public interface IProfileViewService
    {
        ProfileView InitializeProfileView(IAmACommunityMember author);
        void LogError(Exception ex);
    }
}
