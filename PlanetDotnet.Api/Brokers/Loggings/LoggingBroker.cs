// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.Logging;
using System;

namespace PlanetDotnet.Api.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger logger;

        public LoggingBroker(ILogger logger) =>
            this.logger = logger;

        public void LogCritical(Exception exception) =>
            logger.LogCritical(exception, exception.Message);

        public void LogDebug(string message) =>
            logger.LogDebug(message);

        public void LogError(Exception exception) =>
            logger.LogError(exception, exception.Message);

        public void LogInformation(string message) =>
            logger.LogInformation(message);

        public void LogTrace(string message) =>
            logger.LogTrace(message);

        public void LogWarning(string message) =>
            logger.LogWarning(message);
    }
}