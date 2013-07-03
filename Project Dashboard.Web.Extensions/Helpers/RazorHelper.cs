namespace ProjectDashboard.Helpers
{
    using System.Web;

    public static class RazorHelper
    {
        public static IHtmlString ClassAttribute(string className)
        {
            return new HtmlString(string.IsNullOrEmpty(className) ? string.Empty : string.Format(@" class=""{0}""", className));
        }
    }
}
