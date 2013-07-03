namespace Fop
{
    using System;
    using System.IO;
    using System.Text;
    using ApacheFop;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string sourceFo = null,
                   outputFilePath = null;
            foreach (var arg in args)
            {
                var key = arg.Substring(1, arg.IndexOf(":", StringComparison.InvariantCulture) - 1);
                var value = arg.Substring(key.Length + 2);
                switch (key)
                {
                    case "input":
                        sourceFo = File.ReadAllText(value);
                        break;
                    case "fo":
                        var fobytes = Convert.FromBase64String(value);
                        sourceFo = Encoding.Default.GetString(fobytes);
                        break;
                    case "output":
                        outputFilePath = value;
                        break;
                }
            }

            if (sourceFo == null)
            {
                return;
            }

            var e = new Engine();
            var runMethod = e.GetType().GetMethod("Run");
            var spdf = runMethod.Invoke(e, new object[] { sourceFo }) as sbyte[];

            if (spdf == null)
            {
                throw new Exception("Unexpected type returned from transform.");
            }

            var size = spdf.Length;
            var pdf = new byte[size];
            for (var i = 0; i < size; i++)
            {
                pdf[i] = (byte)spdf[i];
            }

            if (outputFilePath == null)
            {
                Console.WriteLine("--pdf start--");
                Console.WriteLine(Convert.ToBase64String(pdf));
                Console.WriteLine("--pdf end--");
            }
            else
            {
                try
                {
                    File.WriteAllBytes(outputFilePath, pdf);
                }
                catch
                {
                    Console.WriteLine("Error writing to file: " + outputFilePath);
                }
            }
        }
    }
}
