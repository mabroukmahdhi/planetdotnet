// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Api.Models.Foundations.Previews;
using PlanetDotnet.Shared.Abstractions;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace PlanetDotnet.Api.Services.Foundations.Authors
{
    public interface IAuthorService
    {
        IEnumerable<IAmACommunityMember> RetrieveAllAuthors();

        ValueTask<IEnumerable<PreviewItem>> RetrieveAllPreviewsAsync();

        Task<SyndicationFeed> RetrieveFeedAsync(
            int? numberOfItems,
            string tag = "all",
            string languageCode = "mixed");

        ValueTask<string> RetrieveXmlFeedAsync(
           int? numberOfItems,
           string tag = "all",
           string languageCode = "mixed");
    }
}
