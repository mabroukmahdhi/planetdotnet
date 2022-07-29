// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
namespace PlanetDotnet.Models.Foundations.Authors.Exceptions
{
    public class AuthorServiceException : Exception
    {
        public AuthorServiceException(Exception innerException) :
            base(message: "Author service error occurred, contact support.", innerException)
        { }
    }

}
