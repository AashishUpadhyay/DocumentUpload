using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Cloud.Language.V1;
using Google.Protobuf.Collections;
using Google.Type;
using GoogleDocument = Google.Cloud.Language.V1.Document;

namespace DocumentUpload
{
    class InvoiceBroker : IInvoiceBroker
    {
        private readonly IInvoiceDatasource _invoiceDatasource;

        public InvoiceBroker() : this(new InvoicetLiteDbDatasource())
        {

        }

        public InvoiceBroker(IInvoiceDatasource invoiceDatasource)
        {
            _invoiceDatasource = invoiceDatasource;
        }

        public Invoice Get(Guid id)
        {
            return _invoiceDatasource.Get(id);
        }

        public IList<Invoice> GetAll()
        {
            return _invoiceDatasource.GetAll();
        }

        public void Save(Document document)
        {
            var invoiceProcessingContext = new InvoiceProcessingContext();
            invoiceProcessingContext.Text = document.Text;
            invoiceProcessingContext.Invoice = new Invoice()
            {
                Id = document.Id
            };

            var tasks = new List<IInvoiceProcessingTask>();

            tasks.Add(new AnalyzeEntities());
            tasks.Add(new DetermineInvoiceAmountDueTask());
            tasks.Add(new DetermineInvoiceTotalAmountTask());
            tasks.Add(new DetermineInvoiceCurrency());
            tasks.Add(new DetermineInvoiceDate());
            InvoiceProcessingTaskExecutionHelper.ExecuteTasks(invoiceProcessingContext, tasks);

            if (invoiceProcessingContext.Invoice.AmountDue == 0)
                invoiceProcessingContext.Invoice.ProcessingStatus = ProcessingStatus.Paid;

            _invoiceDatasource.Save(invoiceProcessingContext.Invoice);
        }
    }
}
