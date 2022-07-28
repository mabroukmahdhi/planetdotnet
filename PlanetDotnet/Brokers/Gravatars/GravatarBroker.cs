// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Foundations.Configurations;

namespace PlanetDotnet.Brokers.Gravatars
{
    public class GravatarBroker : IGravatarBroker
    {
        private readonly LocalConfigurations localConfigurations;
        public GravatarBroker(
            IConfiguration configuration)
        {
            this.localConfigurations =
                configuration.Get<LocalConfigurations>();
        }

        public string GetGravatarImage(IAmACommunityMember member)
        {
            int size = this.localConfigurations?.DefaultGravatarImageSize ?? 200;
            var defaultImage = this.localConfigurations?.DefaultGravatarImage ?? "mm";

            var hash = member.GravatarHash;

            if (string.IsNullOrWhiteSpace(hash))
            {
                return $"{this.localConfigurations.BaseAddress}api/avatar?email={member.EmailAddress}";
            }

            return $"//www.gravatar.com/avatar/{hash}.jpg?s={size}&d={defaultImage}";
        }
    }
}
