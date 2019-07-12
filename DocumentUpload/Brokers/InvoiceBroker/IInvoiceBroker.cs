using System;
using System.Collections.Generic;

namespace DocumentUpload
{
    public interface IInvoiceBroker
    {
        Invoice Get(Guid id);
        IList<Invoice> GetAll();
        void Save(Document document);
    }
}