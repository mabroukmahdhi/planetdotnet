// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace PlanetDotnet.Services.Foundations.Authors.Exceptions
{
    public class NullAuthorException : Exception
    {
        public NullAuthorException()
            : base(message: "Author is null.")
        { }
    }
}
