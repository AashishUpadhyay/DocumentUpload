using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentUpload
{
    class EntityMapper
    {
        public static LightDocument MapDocument(Document input)
        {
            return new LightDocument()
            {
                Id = input.Id,
                UploadedBy = input.UploadedBy,
                UploadTimestamp = input.UploadTimestamp
            };
        }

        public static LightInvoice MapInvoice(Invoice input)
        {
            return new LightInvoice()
            {
                Id = input.Id,
                UploadedBy = input.UploadedBy,
                UploadTimestamp = input.UploadTimestamp,
                AmountDue = input.AmountDue,
                TotalAmount = input.TotalAmount,
                Currency = input.Currency ?? "Unknown",
                ProcessingStatus = input.ProcessingStatus.ToString(),
                InvoiceDate = (input.InvoiceDate == DateTime.MinValue) ? (DateTime?)null : input.InvoiceDate
            };
        }
    }
}
