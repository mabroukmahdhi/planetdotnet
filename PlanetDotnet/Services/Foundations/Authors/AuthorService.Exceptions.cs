// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace PlanetDotnet.Services.Foundations.Authors
{
    public partial class AuthorService
    {
        private delegate string ReturnStringFunction();

        private string TryCatch(ReturnStringFunction returnStringFunction)
        {
            try
            {
                return returnStringFunction();
            }
            //catch (NullAuthorException nullAuthorException)
            //{
            //    throw CreateAndLogValidationException(nullAuthorException);
            //}
            catch (Exception exception)
            {
                throw exception; // throw CreateAndLogServiceException(exception);
            }
        }

        //private AuthorValidationException CreateAndLogValidationException(Exception exception)
        //{
        //    var authorValidationException = new AuthorValidationException(exception);
        //    this.loggingBroker.LogError(authorValidationException);

        //    return authorValidationException;
        //}

        //private AuthorServiceException CreateAndLogServiceException(Exception exception)
        //{
        //    var authorServiceException = new AuthorServiceException(exception);
        //    this.loggingBroker.LogError(authorServiceException);

        //    return authorServiceException;
        //}
    }

}
