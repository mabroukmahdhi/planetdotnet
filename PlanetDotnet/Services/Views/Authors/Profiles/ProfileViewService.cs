// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Views.Authors;
using PlanetDotnet.Services.Foundations.Authors;

namespace PlanetDotnet.Services.Views.Authors.Profiles
{
    public class ProfileViewService : IProfileViewService
    {
        private readonly IAuthorService authorService;

        public ProfileViewService(IAuthorService authorService) =>
             this.authorService = authorService;

        public ProfileView InitializeProfileView(IAmACommunityMember author)
        {
            return new ProfileView
            {
                Id = author.GetType().Name,
                Avatar = RetrieveAvatar(author),
                FullName = $"{author.FirstName} {author.LastName}",
                StateOrRegion = author.StateOrRegion,
                TwitterHandle = author.TwitterHandle,
                WebSite = author.WebSite.ToString(),
                Description = MakeDescription(author)
            };
        }

        public string RetrieveAvatar(IAmACommunityMember author) =>
           this.authorService.RetrieveAuthorImage(author);

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
