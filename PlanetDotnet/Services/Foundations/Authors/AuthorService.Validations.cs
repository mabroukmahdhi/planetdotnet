// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Models.Foundations.Abstractions;

namespace PlanetDotnet.Services.Foundations.Authors
{
    public partial class AuthorService
    {
        private void ValidateAuthor(IAmACommunityMember author)
        {
            switch (author)
            {
                case null:
                    throw new NullAuthorException();
            }
        }
    }
}
