// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Resources;
using System.Globalization;

namespace PlanetDotnet.Brokers.Localizations
{
    public class LocalizationBroker : ILocalizationBroker
    {
        public string GetText(string key)
        {
            return PDResources.ResourceManager.GetString(
                      name: key,
                      culture: CultureInfo.CurrentCulture);
        }
    }
}
