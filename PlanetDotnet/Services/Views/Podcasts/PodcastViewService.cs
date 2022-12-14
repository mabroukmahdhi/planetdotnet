// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Models.Views.Podcasts;
using PlanetDotnet.Shared.Abstractions;
using System;

namespace PlanetDotnet.Services.Views.Podcasts
{
    public class PodcastViewService : IPodcastViewService
    {
        private readonly ILoggingBroker loggingBroker;

        public PodcastViewService(
            ILoggingBroker loggingBroker) =>
            this.loggingBroker = loggingBroker;

        public PodcastView InitializePodcastView(IAmACommunityMember author)
        {
            try
            {
                return new PodcastView
                {
                    Id = $"{author.FirstName}{author.LastName}",
                    Avatar = author.Avatar,
                    FullName = $"{author.FirstName} {author.LastName}",
                    StateOrRegion = author.StateOrRegion,
                    TwitterHandle = author.TwitterHandle,
                    WebSite = author.WebSite.ToString(),
                    Description = MakeDescription(author),
                    Tags = author.Tags
                };

            }
            catch (Exception ex)
            {
                this.loggingBroker.LogError(ex);
                return null;
            }
        }

        private string MakeDescription(IAmACommunityMember author)
        {
            if (string.IsNullOrWhiteSpace(author.ShortBioOrTagLine))
            {
                return string.Empty;
            }

            return $"{author.FirstName} {author.LastName} {author.ShortBioOrTagLine}";
        }
    }
}
