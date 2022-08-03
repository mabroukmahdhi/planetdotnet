﻿// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using PlanetDotnet.Api.Brokers.Authors;
using PlanetDotnet.Api.Brokers.Gravatars;
using PlanetDotnet.Api.Brokers.Loggings;
using PlanetDotnet.Api.Services.Foundations.Authors;
using PlanetDotnet.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;
using Xunit.Abstractions;

namespace PlanetDotnet.Api.Tests.Unit.Services.Foundations.Authors
{
    public partial class AuthorServiceTests
    {
        private readonly Mock<IAuthorBroker> authorBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IGravatarBroker> gravatarBrokerMock;

        private readonly IAuthorService authorService;
        private readonly ITestOutputHelper testOutputHelper;

        private const string ModelsNamespace = "PlanetDotnet.Api.Models.Foundations.Authors";

        public AuthorServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            this.authorBrokerMock = new Mock<IAuthorBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.gravatarBrokerMock = new Mock<IGravatarBroker>();

            this.authorService = new AuthorService(
                authorBroker: this.authorBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                gravatarBroker: this.gravatarBrokerMock.Object);
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
            && actualException.InnerException.Message == expectedException.InnerException.Message;
        }
        private static string[] GetInterfacesNames() => new[]
        {
            nameof(IAmACommunityMember),
            nameof(IAmAFrameworkForDotNet),
            nameof(IAmAMicrosoftMVP),
            nameof(IAmANewsletter),
            nameof(IAmAPodcast),
            nameof(IAmAYoutuber),
            nameof(IWorkAtMicrosoft),
        };

        private static IEnumerable<IAmACommunityMember> GetAuthors()
        {
            var assembly = Assembly.GetAssembly(typeof(IAmACommunityMember));

            var types = assembly.GetTypes();

            var authorTypes = types.Where(type =>
                typeof(IAmACommunityMember).IsAssignableFrom(type)
                && !GetInterfacesNames().Contains(type.Name));

            foreach (var authorType in authorTypes)
            {
                var author = (IAmACommunityMember)Activator.CreateInstance(authorType);
                yield return author;
            }
        }

        public static IEnumerable<object[]> GetAuthorTestData() =>
            GetAuthors().Select(author => new object[] { author });


        private static string GetRandomImage()
        {
            var randomEmail = new EmailAddresses().GetValue();

            var hash = ToMd5Hash(randomEmail);

            return $"//www.gravatar.com/avatar/{hash}.jpg?s=22&d=mm";
        }

        private static string ToMd5Hash(string toHash)
        {
            var unhashedBytes = Encoding.UTF8.GetBytes(toHash);
            var hashedBytes = MD5.Create().ComputeHash(unhashedBytes);

            var hashedString = string.Join(string.Empty,
                hashedBytes.Select(b => b.ToString("X2")).ToArray());
            return hashedString;
        }

        private async Task AuthorHasSecureAndParseableFeed(IAmACommunityMember author)
        {
            try
            {
                foreach (var feedUri in author.FeedUris)
                    Assert.Equal("https", feedUri.Scheme);

                var authors = new[] { author };
                var feed = await this.authorService.RetrieveFeedAsync(null); 

                Assert.NotNull(feed);

                var allItems = feed.Items.Where(i => i != null).ToList();

                Assert.True(allItems?.Count > 0, $"Author {author?.FirstName} {author?.LastName} @{author?.GitHubHandle} doesn't meet post policy {author?.FeedUris?.FirstOrDefault()?.OriginalString}");
            }
            catch (Exception ex)
            {
                testOutputHelper.WriteLine($"Feed(s) for {author.FirstName} {author.LastName} @{author?.GitHubHandle} is null or empty {author?.FeedUris?.FirstOrDefault()?.OriginalString}");

                if (author is IAmAYoutuber youtuber)
                {
                    testOutputHelper.WriteLine("Auhtor is a YouTuber, and will at max have 15 items in feed, ignore empty feed");
                    return;
                }
                else
                {
                    Assert.True(false, $"Feed(s) for {author.FirstName} {author.LastName}  @{author?.GitHubHandle}  is null or empty @{author?.FeedUris?.FirstOrDefault()?.OriginalString}");
                    testOutputHelper.WriteLine($"Feed(s) for {author.FirstName} {author.LastName} is null or empty");
                }

                throw ex;
            }
        }
    }
}
