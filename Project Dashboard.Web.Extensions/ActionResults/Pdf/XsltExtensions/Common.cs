namespace Zone.Library.Mvc.ActionResults.Pdf.XsltExtensions
{
    using System.Text.RegularExpressions;

    public class Common
    {
        public string Urlify(string url, string root)
        {
            return Regex.IsMatch(url, "([a-z]+:)?\\/\\/") ? url : root + url;
        }
    }
}
