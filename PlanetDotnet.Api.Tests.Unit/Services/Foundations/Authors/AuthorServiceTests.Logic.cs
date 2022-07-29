// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using PlanetDotnet.Api.Services.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using System.Linq;
using System.Reflection;
using Xunit;

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

        [Fact]
        public void ShouldImplementInterface()
        {
            var assembly = Assembly.GetAssembly(typeof(IAuthorService));

            var types = assembly.GetTypes();

            var authors = types.Where(type =>
                type.IsClass
                && type.Namespace == ModelsNamespace
                && !type.Name.Contains("<"));

            foreach (var author in authors)
            {
                Assert.True(typeof(IAmACommunityMember).IsAssignableFrom(author),
                    $"{author.Name} does not implement interface {nameof(IAmACommunityMember)}");
            }
        }
    }
}
