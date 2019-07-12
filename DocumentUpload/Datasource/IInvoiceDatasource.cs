using System;
using System.Collections.Generic;

namespace DocumentUpload
{
    internal interface IInvoiceDatasource
    {
        Invoice Get(Guid id);
        IList<Invoice> GetAll();
        Guid Save(Invoice document);
    }
}