﻿// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Models.Foundations.Abstractions;
using PlanetDotnet.Models.Foundations.Configurations;
using PlanetDotnet.Models.Views.Welcomes;
using PlanetDotnet.Services.Foundations.Authors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetDotnet.Services.Views.Welcomes
{
    public class WelcomeViewService : IWelcomeViewService
    {
        private readonly IAuthorService authorService;
        private readonly ILoggingBroker loggingBroker;
        private readonly LocalConfigurations localConfigurations;

        public WelcomeViewService(
            IAuthorService authorService,
            IConfiguration configuration,
            ILoggingBroker loggingBroker)
        {
            this.authorService = authorService;

            this.localConfigurations =
                configuration.Get<LocalConfigurations>();

            this.loggingBroker = loggingBroker;
        }

        public WelcomeView InitializeWelcomeView()
        {
            try
            {
                return new WelcomeView
                {
                    AuthorsPagePath = this.localConfigurations.AuthorsPagePath,
                    BadgeImagePath = this.localConfigurations.BadgeImagePath,
                    FacebookLink = this.localConfigurations.FacebookLink,
                    FeedLink = this.localConfigurations.FeedPagePath,
                    GithubRepository = this.localConfigurations.GithubRepository,
                    TwitterLink = this.localConfigurations.TwitterLink,
                    PreviewPagePath = this.localConfigurations.PreviewPagePath,
                    Podcasts = GetPodcasts()
                };
            }
            catch (Exception ex)
            {
                this.loggingBroker.LogError(ex);

                return null;
            }
        }

        private IEnumerable<IAmACommunityMember> GetPodcasts()
        {
            var authors = this.authorService.RetrieveAllAuthers();

            return authors?.Where(author =>
                author is IAmAPodcast
                || author is IAmANewsletter
                || author is IAmAFrameworkForDotNet);
        }
    }
}