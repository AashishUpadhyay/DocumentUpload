using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace DocumentUpload
{
    class FileInfoLiteDbDatasource : DatasourceBase, IFileInfoDatasource
    {
        public FileInfo Get(Guid fileId)
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var docs = db.GetCollection<FileInfo>("fileinfo");
                return docs.Find(u => u.Id == fileId).FirstOrDefault();
            }
        }

        public IList<FileInfo> GetAll()
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var docs = db.GetCollection<FileInfo>("fileinfo");
                return docs.FindAll().ToList();
            }
        }

        public Guid Save(FileInfo document)
        {
            document.Id = Guid.NewGuid();
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var documents = db.GetCollection<FileInfo>("fileinfo");
                documents.Insert(document);
            }

            return document.Id;
        }
    }
}
