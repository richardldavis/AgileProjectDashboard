namespace ProjectDashboard.Helpers
{
    using System;
    using System.IO;

    public static class ResourceHelper
    {
        public static Stream GetFromResources(Type loadingType, string resourceName)
        {
            var assem = loadingType.Assembly;
            return assem.GetManifestResourceStream(resourceName);
        }
    }
}
