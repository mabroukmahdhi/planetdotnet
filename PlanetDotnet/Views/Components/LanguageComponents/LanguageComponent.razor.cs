// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace PlanetDotnet.Views.Components.LanguageComponents
{
    public partial class LanguageComponent : ComponentBase
    {
        private string currentCulture;

        protected override async Task OnInitializedAsync()
        {
            this.currentCulture =
                 await this.Localizer.GetCurrentCultureAsnyc();

            StateHasChanged();
        }

        private void CultureChanged(ChangeEventArgs args)
        {
            var selected = args.Value.ToString();
            this.Localizer.SetCurrentCultureAsnyc(selected);
        }

    }
}
