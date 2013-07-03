namespace ProjectDashboard.ActionResults.Pdf
{
    using System;
    using System.Linq;
    using System.Xml;
    using Helpers;

    public class EmbeddedStylesheetResolver : XmlResolver
    {
        public string NameFormat { get; private set; }

        public Type CallingType { get; private set; }

        public EmbeddedStylesheetResolver(string nameFormat, Type callingType)
        {
            NameFormat = nameFormat;
            CallingType = callingType;
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            var filename = absoluteUri.Segments.Last().Replace(".xslt", null);
            return ResourceHelper.GetFromResources(CallingType, string.Format(NameFormat, filename));
        }
    }
}
