using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentUpload
{
    class DetermineInvoiceDate : IInvoiceProcessingTask
    {
        public void Execute(InvoiceProcessingContext invoiceProcessingContext)
        {
            if(invoiceProcessingContext.AnalyzeEntitiesResponse == null)
                return;
            var dates = invoiceProcessingContext.AnalyzeEntitiesResponse.Entities.Where(u => Convert.ToInt32(u.Type) == 11 && u.Metadata.ContainsKey("year") && u.Metadata.ContainsKey("month") && u.Metadata.ContainsKey("day"));
            if (dates.Any())
            {
                var invoiceDates = new List<DateTime>();
                dates.ToList().ForEach(u =>
                {
                    invoiceDates.Add(new DateTime(Convert.ToInt32(u.Metadata["year"]), Convert.ToInt32(u.Metadata["month"]), Convert.ToInt32(u.Metadata["day"])));
                });
                invoiceProcessingContext.Invoice.InvoiceDate = invoiceDates.Min();
            }
        }
    }
}
