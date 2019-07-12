namespace DocumentUpload
{
    internal interface IInvoiceProcessingTask
    {
        void Execute(InvoiceProcessingContext invoiceProcessingContext);
    }
}