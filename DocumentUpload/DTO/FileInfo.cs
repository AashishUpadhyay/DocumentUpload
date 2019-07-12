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
    public class FileInfo
    {
        public Guid Id { get; internal set; }
        public int Filesize { get; internal set; }
        public string RelativePath { get; internal set; }
        public string OriginalFileName { get; internal set; }
        public string FileName { get; internal set; }
        public string FileExtension { get; internal set; }
        public byte[] FileData { get; internal set; }
    }
}
