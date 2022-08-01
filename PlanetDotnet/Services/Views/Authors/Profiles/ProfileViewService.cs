// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Models.Views.Authors;
using PlanetDotnet.Services.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using PlanetDotnet.Shared.Abstractions.Tags;
using System;
using System.Linq;

namespace PlanetDotnet.Services.Views.Authors.Profiles
{
    public class ProfileViewService : IProfileViewService
    {
        private readonly IAuthorService authorService;
        private readonly ILoggingBroker loggingBroker;

        public ProfileViewService(
            IAuthorService authorService,
            ILoggingBroker loggingBroker)
        {
            this.authorService = authorService;
            this.loggingBroker = loggingBroker;
        }

        public ProfileView InitializeProfileView(IAmACommunityMember author)
        {
            try
            {
                return new ProfileView
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
                LogError(ex);
                return null;
            }
        }

        public void LogError(Exception ex) =>
            this.loggingBroker.LogError(ex);

        private string MakeDescription(IAmACommunityMember author)
        {
            if (string.IsNullOrWhiteSpace(author.ShortBioOrTagLine))
            {
                return string.Empty;
            }

            if (author is IWorkAtMicrosoft)
            {
                return $"{author.FirstName} {author.LastName} {author.ShortBioOrTagLine} at Microsoft";
            }

            if (author is IAmAMicrosoftMVP)
            {
                return $"{author.FirstName} {author.LastName} {author.ShortBioOrTagLine} and Microsoft MVP";
            }

            return $"{author.FirstName} {author.LastName} {author.ShortBioOrTagLine}";
        }

    }
}
