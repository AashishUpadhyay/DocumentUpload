using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace DocumentUpload
{
    class DocumentLiteDbDatasource : DatasourceBase, IDocumentDatasource
    {
        public Document Get(Guid documentId)
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var docs = db.GetCollection<Document>("documents");
                return docs.Find(u => u.Id == documentId).FirstOrDefault();
            }
        }

        public IList<Document> GetAll()
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var docs = db.GetCollection<Document>("documents");
                return docs.FindAll().ToList();
            }
        }

        public Guid Save(Document document)
        {
            document.Id = Guid.NewGuid();
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var documents = db.GetCollection<Document>("documents");
                documents.Insert(document);
            }

            return document.Id;
        }
    }
}
