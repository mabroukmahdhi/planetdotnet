// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using PlanetDotnet.Brokers.Apis;
using PlanetDotnet.Brokers.Loggings;
using PlanetDotnet.Models.Foundations.Previews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetDotnet.Services.Foundations.Previews
{
    public class PreviewService : IPreviewService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PreviewService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<IEnumerable<PreviewItem>> RetrievePreviewsAsync()
        {
            try
            {
                return await apiBroker.GetPreviewsAsync();
            }
            catch (Exception exception)
            {
                loggingBroker.LogError(exception);

                return null;
            }
        }
    }
}
