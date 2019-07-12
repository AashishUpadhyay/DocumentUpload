using System;
using System.Collections.Generic;

namespace DocumentUpload
{
    public interface IDocumentBroker
    {
        Document Get(Guid id);
        IList<Document> GetAll();
        Guid Save(Document document);
    }
}