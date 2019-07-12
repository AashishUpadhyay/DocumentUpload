using System;
using System.IO;
using System.Web;
using DocumentUpload;
using System.Collections.Generic;

namespace DocumentUpload
{
    class FileRepositoryBroker : IFileRepositoryBroker
    {
        private readonly string _directory;
        private readonly IFileInfoDatasource _fileInfoDatasource;

        public FileRepositoryBroker() : this(new FileInfoLiteDbDatasource())
        {
            _directory = System.Environment.GetEnvironmentVariable("APPDATA") + "\\" + "DocumentUpload" + "\\" +
                            "FileRepository";
            System.IO.Directory.CreateDirectory(_directory);
        }

        public FileRepositoryBroker(IFileInfoDatasource fileInfoDatasource)
        {
            _fileInfoDatasource = fileInfoDatasource;
        }

        public FileInfo Get(Guid id)
        {
            return _fileInfoDatasource.Get(id);
        }

        public IList<FileInfo> GetAll()
        {
            return _fileInfoDatasource.GetAll();
        }

        public Guid Save(FileInfo fileInfo)
        {
            var filePath = _directory + "\\" + fileInfo.FileName + fileInfo.FileExtension;
            System.IO.File.WriteAllBytes(filePath, fileInfo.FileData);
            return _fileInfoDatasource.Save(fileInfo);
        }
    }
}