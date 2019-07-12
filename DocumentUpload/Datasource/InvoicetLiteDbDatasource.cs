using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace DocumentUpload
{
    class InvoicetLiteDbDatasource : DatasourceBase, IInvoiceDatasource
    {
        public Invoice Get(Guid invoiceId)
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var docs = db.GetCollection<Invoice>("invoices");
                return docs.Find(u => u.Id == invoiceId).FirstOrDefault();
            }
        }

        public IList<Invoice> GetAll()
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var docs = db.GetCollection<Invoice>("invoices");
                return docs.FindAll().ToList();
            }
        }

        public Guid Save(Invoice document)
        {
            using (var db = new LiteDatabase(LITE_DB_LOCATION))
            {
                var documents = db.GetCollection<Invoice>("invoices");
                documents.Insert(document);
            }

            return document.Id;
        }
    }
}