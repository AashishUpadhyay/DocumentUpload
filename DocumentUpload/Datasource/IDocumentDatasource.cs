using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentUpload
{
    interface IDocumentDatasource
    {
        Document Get(Guid documentId);

        IList<Document> GetAll();

        Guid Save(Document document);
    }
}
