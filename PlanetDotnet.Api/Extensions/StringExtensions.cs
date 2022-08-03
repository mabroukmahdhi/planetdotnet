// ---------------------------------------------------------------
// Copyright (c) .NET Community, Mabrouk Mahdhi, PlanetXamarin
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;

namespace PlanetDotnet.Api.Extensions
{
    public static class StringExtensions
    {
        private static int MaxLength = 400;

        public static string Sanitize(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return value.StripHtmlTags().Truncate(MaxLength);
        }

        public static string StripHtmlTags(this string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent)) return htmlContent;

            // Strip out any HTML content.
            var strippedContent = Regex.Replace(htmlContent, @"<[^>]*>", String.Empty);
            return strippedContent;
        }

        public static string Truncate(this string value, int maxLength, bool includeLastSentence = true)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value.Length <= maxLength) return value;

            var truncatedContent = value.Substring(0, maxLength);

            if (includeLastSentence && value.IndexOf('.', maxLength) > -1)
            {
                truncatedContent += value.Substring(maxLength, value.IndexOf('.', maxLength) - maxLength + 1);
            }

            return truncatedContent;
        }
    }


    internal static class ExceptionExtensions
    {
        public static TException WithData<TException>(this TException exception, string key, object value) where TException : Exception
        {
            exception.Data[key] = value;
            return exception;
        }
    }

    internal static class SyndicationItemExtensions
    {
        public static bool ApplyDefaultFilter(this SyndicationItem item)
        {
            if (item == null)
                return false;

            var hasXamarinCategory = false;
            var hasXamarinKeywords = false;

            if (item.Categories.Count > 0)
            {
                hasXamarinCategory = item.Categories.Any(category =>
                    category.Name.ToLowerInvariant().Contains("xamarin") || category.Name.ToLowerInvariant().Contains(".net maui"));
            }

            if (item.ElementExtensions.Count > 0)
            {
                var element = item.ElementExtensions.FirstOrDefault(e => e.OuterName == "keywords");
                if (element != null)
                {
                    var keywords = element.GetObject<string>();
                    hasXamarinKeywords = keywords.ToLowerInvariant().Contains("xamarin") || keywords.ToLowerInvariant().Contains(".net maui");
                }
            }

            var hasXamarinTitle = (item.Title?.Text.ToLowerInvariant().Contains("xamarin") ?? false) || (item.Title?.Text.ToLowerInvariant().Contains(".net maui") ?? false);

            return hasXamarinTitle || hasXamarinCategory || hasXamarinKeywords;
        }

        public static string ToHtml(this SyndicationContent content)
        {
            var textSyndicationContent = content as TextSyndicationContent;
            if (textSyndicationContent != null)
            {
                return textSyndicationContent.Text;
            }

            return content.ToString();
        }
    }
}
