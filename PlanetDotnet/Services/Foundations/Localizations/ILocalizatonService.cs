﻿using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Localizations
{
    public interface ILocalizatonService
    {
        string this[string key] { get; }
        bool IsRightToLeft { get; }
        ValueTask SetCurrentCultureAsnyc(string culture);
        ValueTask DeleteCurrentCultureAsnyc();
        ValueTask<string> GetCurrentCultureAsnyc();
    }
}
