// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PlanetDotnet.Api.Services
{
    public class HashService
    {
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
