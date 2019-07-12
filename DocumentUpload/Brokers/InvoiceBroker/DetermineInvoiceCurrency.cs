using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentUpload
{
    class DetermineInvoiceCurrency : IInvoiceProcessingTask
    {
        private static readonly string[] _supportedCurrencies = new string[] { "GBP", "USD", "CAD" };

        public void Execute(InvoiceProcessingContext invoiceProcessingContext)
        {
            if (invoiceProcessingContext.AnalyzeEntitiesResponse == null)
                return;

            var currencies = invoiceProcessingContext.AnalyzeEntitiesResponse.Entities.Where(u => Convert.ToInt32(u.Type) == 13 && u.Metadata.ContainsKey("currency"));
            if (currencies.Any())
            {
                var containedCurrencies = new List<string>();

                foreach (var currency in _supportedCurrencies)
                {
                    if (invoiceProcessingContext.Text.Contains(currency))
                    {
                        containedCurrencies.Add(currency);
                    }
                }

                if (containedCurrencies.Count > 1)
                    invoiceProcessingContext.Invoice.Currency = currencies.GroupBy(u => u.Metadata["currency"]).OrderByDescending(v => v.Count())
                            .FirstOrDefault().Key;
                else
                    invoiceProcessingContext.Invoice.Currency = containedCurrencies.FirstOrDefault();
            }
        }
    }
}
