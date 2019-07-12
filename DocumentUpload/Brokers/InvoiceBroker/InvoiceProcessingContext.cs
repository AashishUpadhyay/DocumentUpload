using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Language.V1;

namespace DocumentUpload
{
    class InvoiceProcessingContext
    {
        public string Text { get; internal set; }

        public Invoice Invoice { get; internal set; }

        public AnalyzeEntitiesResponse AnalyzeEntitiesResponse { get; internal set; }
    }
}
