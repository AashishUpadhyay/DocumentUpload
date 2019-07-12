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
    public class LightDocument
    {
        public Guid Id { get; set; }
      
        public string UploadedBy { get; internal set; }

        public DateTime UploadTimestamp { get; internal set; }
    }
}
