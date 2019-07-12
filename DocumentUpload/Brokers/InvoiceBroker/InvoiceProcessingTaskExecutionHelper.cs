using System.Collections.Generic;
using DocumentUpload;

namespace DocumentUpload
{
    internal static class InvoiceProcessingTaskExecutionHelper
    {
        public static void ExecuteTasks(InvoiceProcessingContext invoiceProcessingContext, IList<IInvoiceProcessingTask> tasks)
        {
            if (tasks == null || tasks.Count == 0)
                return;

            foreach (var task in tasks)
            {
                task.Execute(invoiceProcessingContext);
            }
        }
    }
}
