// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

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
using Xunit.Abstractions;

namespace PlanetDotnet.Api.Tests.Unit.Services.Foundations.Authors
{
    public partial class AuthorServiceTests
    {
        private readonly Mock<IAuthorBroker> authorBrokerMock;
        private readonly IAuthorService authorService;
        private readonly ITestOutputHelper testOutputHelper;

        private const string ModelsNamespace = "PlanetDotnet.Api.Models.Foundations.Authors";

        public AuthorServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            this.authorBrokerMock = new Mock<IAuthorBroker>();

            this.authorService = new AuthorService(
                authorBroker: this.authorBrokerMock.Object);
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
    }
}
