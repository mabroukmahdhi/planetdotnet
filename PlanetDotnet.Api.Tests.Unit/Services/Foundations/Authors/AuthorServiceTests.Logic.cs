// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Services.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Tynamix.ObjectFiller;
using Xunit;
using Xunit.Abstractions;

namespace PlanetDotnet.Api.Tests.Unit.Services.Foundations.Authors
{
    public partial class AuthorServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllAuthors()
        {
            // given
            var authors = GetAuthors();

            this.authorBrokerMock.Setup(broker =>
                broker.SelectAllAuthers())
                    .Returns(authors);

            // when
            var actualAuthors =
                this.authorService.RetrieveAllAuthors();

            // then
            actualAuthors.Should().BeEquivalentTo(authors);

            this.authorBrokerMock.Verify(broker =>
                broker.SelectAllAuthers(),
                    Times.Once);

            this.authorBrokerMock.VerifyNoOtherCalls();
        }
    }
}
