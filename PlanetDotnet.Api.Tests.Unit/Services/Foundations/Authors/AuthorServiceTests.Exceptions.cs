// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using PlanetDotnet.Api.Models.Foundations.Authors.Exceptions;
using System;
using Xunit;

namespace PlanetDotnet.Api.Tests.Unit.Services.Foundations.Authors
{
    public partial class AuthorServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionWhenRetrieveAllIfExceptionOccursAndLogit()
        {
            // given
            var exception = new Exception();

            var expectedServiceExcption =
                new AuthorServiceException(exception);

            this.authorBrokerMock.Setup(broker =>
                broker.SelectAllAuthers())
                    .Throws(exception);

            // when
            var retrieveAllAction = () =>
                this.authorService.RetrieveAllAuthors();

            // then
            retrieveAllAction.Should().Throw<AuthorServiceException>();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedServiceExcption))),
                        Times.Once);

            this.authorBrokerMock.Verify(broker =>
                broker.SelectAllAuthers(),
                    Times.Once);

            this.authorBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
