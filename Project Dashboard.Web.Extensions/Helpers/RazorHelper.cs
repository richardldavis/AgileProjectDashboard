namespace ProjectDashboard.Helpers
{
    using System.Web;

    public static class RazorHelper
    {
        public static IHtmlString ClassAttribute(string className)
        {
            return new HtmlString(string.IsNullOrEmpty(className) ? string.Empty : string.Format(@" class=""{0}""", className));
        }

        public static IHtmlString OpenTag(string name)
        {
            return new HtmlString(string.Format("<{0}>", name));
        }

        public static IHtmlString CloseTag(string name)
        {
            return new HtmlString(string.Format("</{0}>", name));
        }
    }
}
