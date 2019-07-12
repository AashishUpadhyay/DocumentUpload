using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentUpload
{
    class DetermineInvoiceTotalAmountTask : DetermineInvoiceTask
    {
        protected override void BuildAmountDictionary(int index, decimal amount, Dictionary<decimal, List<int>> amountDictionary)
        {
            if (amount > 0)
            {
                if (!amountDictionary.ContainsKey(amount))
                    amountDictionary.Add(amount, new List<int>() { index });
                else
                    amountDictionary[amount].Add(index);
            }
        }

        protected override void SetAmount(InvoiceProcessingContext invoiceProcessingContext, decimal amount)
        {
            invoiceProcessingContext.Invoice.TotalAmount = amount;
        }
    }
}
