// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using PlanetDotnet.Shared.Abstractions;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PlanetDotnet.Api.Brokers.Gravatars
{
    public class GravatarBroker : IGravatarBroker
    {
        public GravatarBroker(
           IConfiguration configuration)
        { }

        public string GetGravatarImage(IAmACommunityMember member)
        {
            int size = 200;
            var defaultImage = "mm";

            var hash = member.GravatarHash;

            if (string.IsNullOrWhiteSpace(hash))
            {
                hash = CreateMd5Hash(member.EmailAddress);
            }

            return $"//www.gravatar.com/avatar/{hash}.jpg?s={size}&d={defaultImage}";
        }

        public string CreateMd5Hash(string email)
        {
            try
            {
                email = email.Trim().ToLowerInvariant();

                var unhashedBytes = Encoding.UTF8.GetBytes(email);
                var hashedBytes = MD5.Create().ComputeHash(unhashedBytes);

                var hashedString = string.Join(string.Empty,
                    hashedBytes.Select(b => b.ToString("X2")).ToArray());

                return hashedString.ToLowerInvariant();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
