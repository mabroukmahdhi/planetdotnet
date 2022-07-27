﻿// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Autofac;
using PlanetDotnet.Models.Foundations.Abstractions;

namespace PlanetDotnet.Models.Modules
{
    public class FeedsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .Where(t => t.IsAssignableTo<IAmACommunityMember>())
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}