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
    public class Document
    {
        private DocumentType _documentType;

        public Document()
        {
            _documentType = DocumentType.Unknown;
        }

        public Guid Id { get; internal set; }

        public string UploadedBy { get; internal set; }

        public DateTime UploadTimestamp { get; internal set; }

        public Guid FileId { get; internal set; }

        public string Text { get; internal set; }

        public DocumentType DocumentType
        {
            get { return _documentType; }
            internal set { _documentType = value; }
        }
    }
}
