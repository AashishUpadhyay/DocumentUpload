using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DocumentUpload
{
    public class LightInvoice : LightDocument
    {
        public DateTime? InvoiceDate { get; internal set; }
        public decimal TotalAmount { get; internal set; }
        public decimal AmountDue { get; internal set; }
        public string Currency { get; internal set; }
        public string ProcessingStatus { get; internal set; }
    }
}
