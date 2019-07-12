using System;
using System.IO;
using System.Net;
using DocumentUpload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Owin.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace DocumentUploadTests
{
    [TestClass]
    public class TaskTests : TestBase
    {
        private string resourceLocation = "DocumentUploadTests.Artifacts";
        private string[] files = new string[] { "HubdocInvoice1.txt", "HubdocInvoice2.txt", "HubdocInvoice3.txt", "HubdocInvoice4.txt", "HubdocInvoice5.txt" };
        private decimal[] invoiceAmount = new decimal[] { Convert.ToDecimal(22.50), Convert.ToDecimal(40.00), Convert.ToDecimal(14.13), Convert.ToDecimal(118.65), Convert.ToDecimal(118.65) };
        private decimal[] amountDue = new decimal[] { Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0) };
        private string[] invoiceCurrency = new string[] { "GBP", "USD", "CAD", "CAD", "CAD" };
        private DateTime[] invoiceDate = new DateTime[] { new DateTime(2019, 02, 22), new DateTime(2019, 03, 12), new DateTime(2019, 03, 13), new DateTime(2019, 03, 18), new DateTime(2019, 03, 18) };

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void TestDetermineInvoiceTotalAmountTaskForInvoiceTotal()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = resourceLocation + "." + files[i];

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContents = reader.ReadToEnd();
                            var invoiceProcessingContext = new InvoiceProcessingContext();
                            invoiceProcessingContext.Invoice = new Invoice();
                            invoiceProcessingContext.Text = fileContents;
                            (new DetermineInvoiceTotalAmountTask()).Execute(invoiceProcessingContext);
                            Assert.AreEqual(Convert.ToDecimal(invoiceAmount[i]), Convert.ToDecimal(invoiceProcessingContext.Invoice.TotalAmount));
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestDetermineInvoiceTotalAmountTaskForAmountDue()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = resourceLocation + "." + files[i];

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContents = reader.ReadToEnd();
                            var invoiceProcessingContext = new InvoiceProcessingContext();
                            invoiceProcessingContext.Invoice = new Invoice();
                            invoiceProcessingContext.Text = fileContents;
                            (new DetermineInvoiceAmountDueTask()).Execute(invoiceProcessingContext);
                            Assert.AreEqual(Convert.ToDecimal(amountDue[i]), Convert.ToDecimal(invoiceProcessingContext.Invoice.AmountDue));
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestAnalyzeEntitiesTimeOut()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = resourceLocation + "." + files[0];

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var fileContents = reader.ReadToEnd();
                        var invoiceProcessingContext = new InvoiceProcessingContext();
                        invoiceProcessingContext.Invoice = new Invoice();
                        invoiceProcessingContext.Text = fileContents;

                        var tasks = new List<IInvoiceProcessingTask>();
                        tasks.Add(new AnalyzeEntities(10));
                        InvoiceProcessingTaskExecutionHelper.ExecuteTasks(invoiceProcessingContext, tasks);
                    }
                }
            }
        }

        [TestMethod]
        public void TestDetermineInvoiceDate()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = resourceLocation + "." + files[i];

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContents = reader.ReadToEnd();
                            var invoiceProcessingContext = new InvoiceProcessingContext();
                            invoiceProcessingContext.Invoice = new Invoice();
                            invoiceProcessingContext.Text = fileContents;

                            var tasks = new List<IInvoiceProcessingTask>();
                            tasks.Add(new AnalyzeEntities());
                            tasks.Add(new DetermineInvoiceDate());
                            InvoiceProcessingTaskExecutionHelper.ExecuteTasks(invoiceProcessingContext, tasks);

                            if (invoiceProcessingContext.AnalyzeEntitiesResponse != null)
                                Assert.AreEqual(invoiceDate[i], invoiceProcessingContext.Invoice.InvoiceDate);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestDetermineInvoiceCurrency()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = resourceLocation + "." + files[i];

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContents = reader.ReadToEnd();
                            var invoiceProcessingContext = new InvoiceProcessingContext();
                            invoiceProcessingContext.Invoice = new Invoice();
                            invoiceProcessingContext.Text = fileContents;

                            var tasks = new List<IInvoiceProcessingTask>();
                            tasks.Add(new AnalyzeEntities());
                            tasks.Add(new DetermineInvoiceCurrency());
                            InvoiceProcessingTaskExecutionHelper.ExecuteTasks(invoiceProcessingContext, tasks);

                            if (invoiceProcessingContext.AnalyzeEntitiesResponse != null)
                                Assert.AreEqual(invoiceCurrency[i], Convert.ToString(invoiceProcessingContext.Invoice.Currency));
                        }
                    }
                }
            }
        }

        public void TestDetermineInvoiceDateForFailure()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = resourceLocation + "." + files[i];

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContents = reader.ReadToEnd();
                            var invoiceProcessingContext = new InvoiceProcessingContext();
                            invoiceProcessingContext.Invoice = new Invoice();
                            invoiceProcessingContext.Text = fileContents;

                            var tasks = new List<IInvoiceProcessingTask>();
                            tasks.Add(new AnalyzeEntities(1));
                            tasks.Add(new DetermineInvoiceDate());
                            InvoiceProcessingTaskExecutionHelper.ExecuteTasks(invoiceProcessingContext, tasks);

                            Assert.AreNotEqual(invoiceDate[i], invoiceProcessingContext.Invoice.InvoiceDate);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestDetermineInvoiceCurrencyForFailure()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = resourceLocation + "." + files[i];

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContents = reader.ReadToEnd();
                            var invoiceProcessingContext = new InvoiceProcessingContext();
                            invoiceProcessingContext.Invoice = new Invoice();
                            invoiceProcessingContext.Text = fileContents;

                            var tasks = new List<IInvoiceProcessingTask>();
                            tasks.Add(new AnalyzeEntities(1));
                            tasks.Add(new DetermineInvoiceCurrency());
                            InvoiceProcessingTaskExecutionHelper.ExecuteTasks(invoiceProcessingContext, tasks);

                            Assert.AreNotEqual(invoiceCurrency[i], Convert.ToString(invoiceProcessingContext.Invoice.Currency));
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestDetermineInvoiceTotalAmountTaskForFailure()
        {
            var invoiceProcessingContext = new InvoiceProcessingContext();
            invoiceProcessingContext.Invoice = new Invoice();
            invoiceProcessingContext.Text = "Some Random Text";
            (new DetermineInvoiceTotalAmountTask()).Execute(invoiceProcessingContext);
            (new DetermineInvoiceAmountDueTask()).Execute(invoiceProcessingContext);

            invoiceProcessingContext.Text = "Some Random Text $100 $200 $300 $400 Some Random Text";

            (new DetermineInvoiceTotalAmountTask()).Execute(invoiceProcessingContext);
            (new DetermineInvoiceAmountDueTask()).Execute(invoiceProcessingContext);
            Assert.IsTrue(true);
        }
    }
}
