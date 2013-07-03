[assembly: System.Web.UI.WebResource("ProjectDashboard.Web.ActionResults.Pdf.XsltFiles.xslfo-article.xslt", "text/xml")]
[assembly: System.Web.UI.WebResource("ProjectDashboard.Web.ActionResults.Pdf.XsltFiles.xslfo-common.xslt", "text/xml")]

namespace Zone.Library.Mvc.ActionResults.Pdf
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Xsl;
    using Mvp.Xml.Common.Xsl;
    using ProjectDashboard.ActionResults.Pdf;
    using ProjectDashboard.Helpers;

    public class PdfResult : ActionResult
    {
        #region Fields

        private const string StylesheetNameFormat = "ProjectDashboard.ActionResults.Pdf.XsltFiles.{0}.xslt";

        #endregion

        #region Properties

        public string HtmlContent { get; set; }

        public string FileName { get; set; }
        
        #endregion

        #region Methods

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (HtmlContent == null)
            {
                throw new NullReferenceException("HtmlContent cannot be null.");
            }

            var fopPath = context.HttpContext.Server.MapPath("~/bin/fop/Fop.exe");
            var fo = GenerateFo(HtmlContent);
            var pdf = GeneratePdf(fo, fopPath, context);

            var response = context.HttpContext.Response;
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition", string.Format(@"attachment; filename=""{0}""", FileName ?? "attachment.pdf"));
            response.BinaryWrite(pdf);
        }

        #endregion

        #region Helpers

        private static byte[] GenerateFo(string htmlContent)
        {
            var pageReader = new StringReader(htmlContent);
            var htmlReader = XmlReader.Create(pageReader, new XmlReaderSettings{DtdProcessing = DtdProcessing.Parse});

            var xsl = new XmlDocument();
            using (var xslReader = XmlReader.Create(GetXsltStreamFromResources("xslfo-article"), new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore }))
            {
                xsl.Load(xslReader);
            }

            var foWriterSettings = new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Auto };
            var foOutput = new StringWriter();
            var foWriter = XmlWriter.Create(foOutput, foWriterSettings);

            var xslt = new MvpXslTransform();
            xslt.Load(xsl, XsltSettings.TrustedXslt, new EmbeddedStylesheetResolver(StylesheetNameFormat, typeof(PdfResult)));
            xslt.EnforceXHTMLOutput = false;

            var arguments = new XsltArgumentList();
            arguments.AddExtensionObject("urn:dash.common", new XsltExtensions.Common());
            arguments.AddParam("root-url", string.Empty, "http://www.bupa.com/");
            arguments.AddParam("highlight-colour", string.Empty, "#cccccc");
            arguments.AddParam("include-images", string.Empty, false);

            try
            {
                xslt.Transform(new XmlInput(htmlReader), arguments, new XmlOutput(foWriter));
            }
            finally
            {
                htmlReader.Close();
                foWriter.Close();
            }

            var fo = foOutput.ToString();
            return Encoding.Default.GetBytes(fo);
        }

        private static byte[] GeneratePdf(byte[] fo, string fopPath, ControllerContext context)
        {
            var tempFoPath = context.RequestContext.HttpContext.Server.MapPath("~/App_Data/TEMP/fo.fo");
            FileSystemHelper.SaveAsFile(tempFoPath, fo);
            var compiler = new Process
                               {
                                   StartInfo =
                                       {
                                           FileName = fopPath,
                                           Arguments = string.Format(@"/input:""{0}""", tempFoPath),
                                           UseShellExecute = false,
                                           RedirectStandardOutput = true,
                                       }
                               };

            compiler.Start();
            var output = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            FileSystemHelper.DeleteFile(tempFoPath);

            const string startFlag = "--pdf start--",
                         endFlag = "--pdf end--";
            var pdf = Convert.FromBase64String(output.Substring(0, output.IndexOf(endFlag, StringComparison.InvariantCulture))
                                                     .Substring(output.IndexOf(startFlag, StringComparison.InvariantCulture) + startFlag.Length)
                                                     .Trim());

            return pdf;
        }

        private static Stream GetXsltStreamFromResources(string xsltName)
        {
            var resourceName = string.Format(StylesheetNameFormat, xsltName);
            return ResourceHelper.GetFromResources(typeof(PdfResult), resourceName);
        }

        #endregion
    }
}
