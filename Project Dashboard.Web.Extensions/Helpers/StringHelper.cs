namespace ProjectDashboard.Helpers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    public static class StringHelper
    {
        public static string Capitalise(this string input)
        {
            return !string.IsNullOrEmpty(input) ? input.Substring(0, 1).ToUpper() + input.Substring(1) : string.Empty;
        }

        public static IHtmlString RenumberHeadings(this IHtmlString input, int minimumRequiredLevel)
        {
            if (minimumRequiredLevel < 1 || minimumRequiredLevel > 6)
            {
                throw new ArgumentException("Minimum required header level must be between 1 and 6.");
            }

            var rawInput = input.ToHtmlString();
            var existingHeaders = Regex.Matches(rawInput, @"<h(?<level>\d)(\s|>)").Cast<Match>().ToArray();
            if (!existingHeaders.Any())
            {
                return input;
            }

            var minimumExistingLevel = existingHeaders.Select(i => int.Parse(i.Groups["level"].Value)).Min();
            var output = rawInput;
            for (var i = 6; i >= 1; i--)
            {
                output = Regex.Replace(output, string.Format(@"<(?<slash>/?)h{0}(?<next>\s|>)", i), string.Format("<${{slash}}h{0}${{next}}", Math.Min(6, i + minimumRequiredLevel - minimumExistingLevel)));
            }

            return new HtmlString(output);
        }
    }
}
