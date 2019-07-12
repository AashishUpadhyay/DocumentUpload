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
    public enum DocumentType
    {
        Unknown = 0,
        Invoice = 1,
        Receipt = 2,
    }
}
