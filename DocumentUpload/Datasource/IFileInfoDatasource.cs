using System;
using System.Collections.Generic;

namespace DocumentUpload
{
    internal interface IFileInfoDatasource
    {
        FileInfo Get(Guid fileId);

        IList<FileInfo> GetAll();

        Guid Save(FileInfo fileInfo);
    }
}