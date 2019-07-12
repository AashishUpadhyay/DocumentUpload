using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentUpload
{
    class DocumentBroker : IDocumentBroker
    {
        private readonly IDocumentDatasource _documentDatasource;

        public DocumentBroker() : this(new DocumentLiteDbDatasource())
        {

        }

        public DocumentBroker(IDocumentDatasource documentDatasource)
        {
            _documentDatasource = documentDatasource;
        }

        public Document Get(Guid id)
        {
            return _documentDatasource.Get(id);
        }

        public IList<Document> GetAll()
        {
            return _documentDatasource.GetAll();
        }

        public Guid Save(Document document)
        {
            document.Id = _documentDatasource.Save(document);
            SaveDocumentAdditionalDetails(document);
            return document.Id;
        }

        private void SaveDocumentAdditionalDetails(Document document)
        {
            switch (document.DocumentType)
            {
                case DocumentType.Invoice:
                    (new InvoiceBroker()).Save(document);
                    break;
                case DocumentType.Receipt:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
