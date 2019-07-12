using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace DocumentUpload
{
    public interface IFileRepositoryBroker
    {
        FileInfo Get(Guid id);
        IList<FileInfo> GetAll();
        Guid Save(FileInfo fileInfo);
    }
}