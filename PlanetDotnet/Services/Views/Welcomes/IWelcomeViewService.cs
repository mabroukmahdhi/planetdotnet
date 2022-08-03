// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Views.Welcomes;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Views.Welcomes
{
    public interface IWelcomeViewService
    {
        ValueTask<WelcomeView> InitializeWelcomeViewAsync();
    }
}
