using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Language.V1;

namespace DocumentUpload
{
    class AnalyzeEntities : IInvoiceProcessingTask
    {
        private int _apiTimeout;

        public AnalyzeEntities(int apiTimeout = 30000)
        {
            _apiTimeout = apiTimeout;
        }

        public void Execute(InvoiceProcessingContext invoiceProcessingContext)
        {
            bool executionStarted = false;
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            var executionTask = new System.Threading.Tasks.Task(() =>
            {
                RetrieveEntities(invoiceProcessingContext);
            }, token);


            while (!(executionTask.IsCompleted || token.IsCancellationRequested))
            {
                if (!executionStarted)
                {
                    executionStarted = true;
                    executionTask.Start();
                    Task.Run(() =>
                      {
                          if (TimedOut())
                          {
                              tokenSource.Cancel();
                          }
                      });
                }
            }
        }

        private bool TimedOut()
        {
            Thread.Sleep(_apiTimeout);
            return true;
        }

        private void RetrieveEntities(InvoiceProcessingContext invoiceProcessingContext)
        {
            var client = LanguageServiceClient.Create();
            invoiceProcessingContext.AnalyzeEntitiesResponse = client.AnalyzeEntities(new Google.Cloud.Language.V1.Document()
            {
                Content = invoiceProcessingContext.Text,
                Type = Google.Cloud.Language.V1.Document.Types.Type.PlainText
            });
        }
    }
}