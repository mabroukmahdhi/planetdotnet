// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Foundations.Configurations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PlanetDotnet.Brokers.Gravatars
{
    public class GravatarBroker : IGravatarBroker
    {
        private readonly LocalConfigurations localConfigurations;

        public GravatarBroker(IConfiguration configuration) =>
            this.localConfigurations =
                configuration.Get<LocalConfigurations>();

        public string GetGravatarImage(IAmACommunityMember member)
        {
            int size = this.localConfigurations?.DefaultGravatarImageSize ?? 200;

            var hash = member.GravatarHash;

            if (string.IsNullOrWhiteSpace(hash))
            {
                hash = member.EmailAddress.Trim().ToLowerInvariant();

                hash = ToMd5Hash(hash).ToLowerInvariant();
            }

            var defaultImage = this.localConfigurations?.DefaultGravatarImage ?? "mm";

            return $"//www.gravatar.com/avatar/{hash}.jpg?s={size}&d={defaultImage}";
        }

        private static string ToMd5Hash(string toHash)
        {
            var unhashedBytes = Encoding.UTF8.GetBytes(toHash);
            var hashedBytes = MD5.Create().ComputeHash(unhashedBytes);

            var hashedString = string.Join(string.Empty,
                hashedBytes.Select(b => b.ToString("X2")).ToArray());
            return hashedString;
        }
    }

}
