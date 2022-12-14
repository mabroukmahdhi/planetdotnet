// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace PlanetDotnet.Shared.Abstractions.Tags
{
    public struct Tag
    {
        private readonly string value;

        private Tag(string value) =>
             this.value = value;

        public static Tag AspNetCore => new("ASP.NET Core");
        public static Tag WebAPIs => new("Web APIs");
        public static Tag Blazor => new("Blazor");
        public static Tag Microservices => new("Microservices");
        public static Tag DotNetMAUI => new(".NET MAUI");
        public static Tag WindowsForms => new("Windows Forms");
        public static Tag WinUI => new("WinUI");
        public static Tag WPF => new("WPF");
        public static Tag Xamarin => new("Xamarin");
        public static Tag Cloud => new("Cloud");
        public static Tag MachineLearningAndAI => new("Machine Learning & AI");
        public static Tag GameDevelopment => new("Game Development");
        public static Tag IoT => new("IoT");
        public static Tag TheStandard => new("The Standard");
        public static Tag Default => new(".NET");

        public static implicit operator string(Tag tag) => tag.value;
        public static explicit operator Tag(string tag) => new(tag);

        public override string ToString() =>
            value;
    }
}
