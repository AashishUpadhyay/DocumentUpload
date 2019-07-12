using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocumentUpload
{
    public class DocumentController : ApiController
    {
        public LightDocument Get(Guid id)
        {
            var document = BrokerFactory.Instance.DocumentBroker.Get(id);
            if (document == null) return null;
            return WithAdditionalInfo(document);
        }

        private LightDocument WithAdditionalInfo(Document returnValue)
        {
            switch (returnValue.DocumentType)
            {
                case DocumentType.Invoice:
                    var lightDoc = EntityMapper.MapDocument(returnValue);
                    var invoice = BrokerFactory.Instance.InvoiceBroker.Get(returnValue.Id);
                    if (invoice != null)
                    {
                        invoice.DocumentType = DocumentType.Invoice;
                        invoice.UploadTimestamp = lightDoc.UploadTimestamp;
                        invoice.UploadedBy = lightDoc.UploadedBy;
                        return EntityMapper.MapInvoice(invoice);
                    }
                    return lightDoc;
                    break;
                case DocumentType.Receipt:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException();
            }
        }

        public IList<LightDocument> Get()
        {
            var docs = BrokerFactory.Instance.DocumentBroker.GetAll().Select(WithAdditionalInfo).ToList();
            return docs;
        }
    }
}
