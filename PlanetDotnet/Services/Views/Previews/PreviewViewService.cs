// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Previews;
using PlanetDotnet.Models.Views.Previews;
using PlanetDotnet.Services.Foundations.Previews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Views.Previews
{
    public class PreviewViewService : IPreviewViewService
    {
        private readonly IPreviewService previewService;

        public PreviewViewService(IPreviewService previewService) =>
            this.previewService = previewService;

        public async ValueTask<IEnumerable<PreviewItemView>> LoadPreviewsAsync()
        {
            try
            {
                var views = new List<PreviewItemView>();
                var previews = await this.previewService.RetrievePreviewsAsync();

                if (previews == null)
                {
                    return views;
                }

                foreach (var preview in previews)
                {
                    views.Add(MapToView(preview));
                }

                return views;
            }
            catch
            {
                return new List<PreviewItemView>();
            }
        }

        private PreviewItemView MapToView(PreviewItem preview) =>
            new PreviewItemView
            {
                AuthorName = preview.AuthorName,
                Body = preview.Body,
                Gravatar = preview.Gravatar,
                Link = preview.Link,
                PublishDate = preview.PublishDate,
                Title = preview.Title
            };
    }
}
