// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace PlanetDotnet.Api.Models.Foundations.Feeds.Exceptions
{

    public class FailedFeedServiceException : Exception
    {
        public FailedFeedServiceException(string message)
             : base(message)
        {
        }

        public FailedFeedServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
