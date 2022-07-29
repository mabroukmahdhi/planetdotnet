// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace PlanetDotnet.Models.Foundations.Tags
{
    public struct Tag
    {
        private readonly string value;

        private Tag(string value) =>
             this.value = value;

        public static Tag AspNetCore => new Tag("ASP.NET Core");
        public static Tag WebAPIs => new Tag("Web APIs");
        public static Tag Blazor => new Tag("Blazor");
        public static Tag Microservices => new Tag("Microservices");
        public static Tag DotNetMAUI => new Tag(".NET MAUI");
        public static Tag WindowsForms => new Tag("Windows Forms");
        public static Tag WinUI => new Tag("WinUI");
        public static Tag WPF => new Tag("WPF");
        public static Tag Xamarin => new Tag("Xamarin");
        public static Tag Cloud => new Tag("Cloud");
        public static Tag MachineLearningAndAI => new Tag("Machine Learning & AI");
        public static Tag GameDevelopment => new Tag("Game Development");
        public static Tag IoT => new Tag("IoT");
        public static Tag Default => new Tag(".NET");

        public static implicit operator string(Tag tag) => tag.value;
        public static explicit operator Tag(string tag) => new Tag(tag);

        public override string ToString() =>
            this.value;
    }
}
