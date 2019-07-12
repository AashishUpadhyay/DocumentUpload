using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentUpload
{
    class DetermineInvoiceTask: IInvoiceProcessingTask
    {
        public void Execute(InvoiceProcessingContext invoiceProcessingContext)
        {
            decimal returnValue = 0;
            MatchCollection matches = Regex.Matches(invoiceProcessingContext.Text, @"((\+|\-)?[$€£])\s*(\d[.\d,]*)");

            var amounts = new List<decimal>();
            var amountDictionary = new Dictionary<decimal, List<int>>();

            for (int i = 0; i < matches.Count; i++)
            {
                var str = matches[i].ToString();
                var num = 0;
                var formattedAmountAsString = str.Replace("$", "").Replace("£", "");
                decimal amount = 0;
                if (decimal.TryParse(formattedAmountAsString, out amount))
                    amounts.Add(amount);

                BuildAmountDictionary(i, amount, amountDictionary);
            }

            SetIndexForAmountsToWhichTheyCanBeFormedByAddingOtherAmountsConsecutively(amounts, amountDictionary);

            int maxIndex = 0;
            foreach (var item in amountDictionary)
            {
                foreach (var index in item.Value)
                {
                    if (index > maxIndex)
                    {
                        maxIndex = index;
                        returnValue = item.Key;
                    }
                }
            }

            SetAmount(invoiceProcessingContext, returnValue);
        }

        protected virtual void BuildAmountDictionary(int index, decimal amount, Dictionary<decimal, List<int>> amountDictionary)
        {
            throw new NotImplementedException();
        }

        protected virtual void SetAmount(InvoiceProcessingContext invoiceProcessingContext, decimal amount)
        {
            throw new NotImplementedException();
        }

        private void SetIndexForAmountsToWhichTheyCanBeFormedByAddingOtherAmountsConsecutively(List<decimal> amounts, Dictionary<decimal, List<int>> dictionary)
        {
            var keyItems = new Dictionary<decimal, int>();

            dictionary.Keys.ToList().ForEach(u =>
            {
                keyItems.Add(u, 0);
            });

            foreach (var key in dictionary.Keys)
            {
                foreach (var index in dictionary[key])
                {
                    keyItems[key] = SetIndexForAmount(amounts, key, index);
                }
            }
        }

        private int SetIndexForAmount(List<decimal> amounts, decimal key, int currentIndex)
        {
            decimal runningTotal = 0;
            int index = 0;

            for (int i = 0; i < amounts.Count; i++)
            {
                if (i != currentIndex)
                {
                    runningTotal += amounts[i];
                    if (runningTotal == key)
                        index = i;
                }
            }
            return index;
        }
    }
}
