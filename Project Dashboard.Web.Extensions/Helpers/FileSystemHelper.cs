namespace ProjectDashboard.Helpers
{
    using System.IO;
    using System.Text;

    public static class FileSystemHelper
    {
        public static void SaveAsFile(string absolutePath, byte[] data)
        {
            var directoryPath = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(absolutePath, data);
        }

        public static void SaveAsFile(string absolutePath, string data)
        {
            SaveAsFile(absolutePath, Encoding.UTF8.GetBytes(data));
        }

        public static void DeleteFile(string absolutePath)
        {
            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }
        }
    }
}
