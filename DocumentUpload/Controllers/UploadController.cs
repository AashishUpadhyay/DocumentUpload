using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DocumentUpload
{
    public class UploadController : ApiController
    {
        private static string[] _supportedExtensions = new string[] { ".pdf", ".txt" };

        public object Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));

            var document = ProcessRequest();
            return new { id = document.Id };
        }

        private Document ProcessRequest()
        {
            var fileInfo = new FileInfo();
            var document = new Document();

            var provider = new MultipartMemoryStreamProvider();
            var res = Request.Content.ReadAsMultipartAsync(provider).Result;

            for (int i = 0; i < res.Contents.Count; i++)
            {
                var content = res.Contents[i];

                if (content.Headers.ContentDisposition.Name.ToLowerInvariant().Contains("email"))
                {
                    document.UploadedBy = content.ReadAsStringAsync().Result;
                    document.UploadTimestamp = DateTime.Now;
                }

                if (content.Headers.ContentDisposition.Name.ToLowerInvariant().Contains("file"))
                {
                    var originalFileName = string.Empty;
                    if (!string.IsNullOrEmpty(content.Headers.ContentDisposition.FileName))
                        fileInfo.OriginalFileName = content.Headers.ContentDisposition.FileName.Trim('\"');

                    fileInfo.FileName = System.Guid.NewGuid().ToString();
                    fileInfo.FileExtension = (new System.IO.FileInfo(fileInfo.OriginalFileName)).Extension;

                    if (!_supportedExtensions.Contains(fileInfo.FileExtension))
                        throw new InvalidOperationException("Unsupported File Type!");

                    fileInfo.FileData = content.ReadAsByteArrayAsync().Result;
                }

                if (content.Headers.ContentDisposition.Name.ToLowerInvariant().Equals("type"))
                {
                    int documentType = 0;
                    if (int.TryParse(content.ReadAsStringAsync().Result, out documentType))
                        document.DocumentType = (DocumentType)documentType;
                }
            }

            if (document.DocumentType == DocumentType.Unknown)
                document.DocumentType = DocumentType.Invoice;

            var fileId = BrokerFactory.Instance.FileRepositoryBroker.Save(fileInfo);
            document.FileId = fileId;

            if (fileInfo.FileExtension == ".pdf")
                document.Text = Utility.ConvertPDFToText(fileInfo.FileData);
            else
                document.Text = System.Text.Encoding.UTF8.GetString(fileInfo.FileData);
            BrokerFactory.Instance.DocumentBroker.Save(document);
            return document;
        }
    }
}
