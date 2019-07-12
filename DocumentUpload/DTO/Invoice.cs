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
    public class Invoice : Document
    {
        public string VendorName { get; internal set; }
        public DateTime InvoiceDate { get; internal set; }
        public decimal AmountDue { get; internal set; }
        public string Currency { get; internal set; }
        public decimal TaxAmount { get; internal set; }
        public decimal TotalAmount { get; internal set; }
        public ProcessingStatus ProcessingStatus { get; internal set; }
    }
}
