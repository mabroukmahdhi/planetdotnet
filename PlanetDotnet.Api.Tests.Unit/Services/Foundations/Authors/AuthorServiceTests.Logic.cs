// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using PlanetDotnet.Api.Services.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using PlanetDotnet.Shared.Abstractions.GeoPositions;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

            var randomImage = GetRandomImage();

            this.authorBrokerMock.Setup(broker =>
                broker.SelectAllAuthers())
                    .Returns(authors);

            this.gravatarBrokerMock.Setup(broker =>
                broker.GetGravatarImage(
                    It.IsAny<IAmACommunityMember>()))
                      .Returns(randomImage);

            // when
            var actualAuthors =
                this.authorService.RetrieveAllAuthors();

            // then
            actualAuthors.Should().BeEquivalentTo(authors);

            foreach (var item in actualAuthors)
            {
                item.Avatar.Should().NotBeNullOrWhiteSpace();
            }

            this.authorBrokerMock.Verify(broker =>
                broker.SelectAllAuthers(),
                    Times.Once);

            this.gravatarBrokerMock.Verify(broker =>
                broker.GetGravatarImage(
                    It.IsAny<IAmACommunityMember>()),
                        Times.Exactly(authors.Count()));

            this.authorBrokerMock.VerifyNoOtherCalls();
            this.gravatarBrokerMock.VerifyNoOtherCalls();
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

        [Fact]
        public void ShouldBeInAuthorsNamespace()
        {
            var assembly = Assembly.GetAssembly(typeof(IAmACommunityMember));

            var types = assembly.GetTypes();

            var authors = types.Where(type =>
                type.IsClass
                && type.Namespace == ModelsNamespace
                && !type.Name.Contains("<"));

            foreach (var author in authors)
            {
                this.testOutputHelper.WriteLine($"{author.Name} uses Namespace: {author.Namespace}");

                author.Namespace.Should().Be(ModelsNamespace);
            }
        }

        [Fact]
        public void ShouldHaveValidLanguageCode()
        {
            // given
            var authors = GetAuthors();

            var cultureNames = CultureInfo.GetCultures(
             types: CultureTypes.NeutralCultures)
                 .Select(c => c.Name);

            // when .. then

            foreach (var author in authors)
            {
                cultureNames.Should().Contain(author.FeedLanguageCode);
            }
        }

        [Fact]
        public void ShouldHaveFirstName()
        {
            // given
            var authors = GetAuthors();

            // when .. then
            foreach (var author in authors)
            {
                author.FirstName.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void ShouldHaveValidWebSite()
        {
            // given
            var authors = GetAuthors();

            // when .. then
            foreach (var author in authors)
            {
                author.WebSite.Should().NotBeNull();
                author.WebSite.IsWellFormedOriginalString().Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldHaveValidCoordinates()
        {
            // given
            var authors = GetAuthors();

            // when .. then
            foreach (var author in authors)
            {
                if (author.Position == null)
                    continue;

                if (author.Position == GeoPosition.Empty)
                    continue;

                author.Position.Lat.Should().BeInRange(-90.0, 90);
                author.Position.Lng.Should().BeInRange(-180.0, 180);
            }
        }

        [Fact]
        public void ShouldNotDefineAvatarProperty()
        {
            // given
            var authors = GetAuthors();

            // when .. then
            foreach (var author in authors)
            {
                author.Avatar.Should().NotBeNull();
            }
        }

        [Fact]
        public Task ShouldHaveSecureAndParsableFeed()
        {
            // given
            var authors = GetAuthors();

            this.authorBrokerMock.Setup(broker =>
               broker.SelectAllAuthers())
                   .Returns(authors);

            // using MemberData for this test is slow. Intentionally using Task.WhenAll here!
            return Task.WhenAll(authors.Select(AuthorHasSecureAndParseableFeed));
        }
    }
}
