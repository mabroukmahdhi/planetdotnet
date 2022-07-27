// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace PlanetDotnet.Models.Foundations.Authors.Exceptions
{
    public class AuthorValidationException : Exception
    {
        public AuthorValidationException(Exception innerException)
            : base(message: "Author validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
