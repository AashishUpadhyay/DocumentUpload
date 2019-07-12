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
    public class UploadControllerTest : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            var uploadController = new UploadController();
        }

        private string resourceLocation = "DocumentUploadTests.Artifacts";
        private string[] files = new string[] { "HubdocInvoice2.pdf", "HubdocInvoice1.pdf", "HubdocInvoice3.pdf", "HubdocInvoice4.pdf", "HubdocInvoice5.pdf" };

        [TestMethod]
        public void Upload()
        {
            foreach (var file in files)
            {
                using (var server = new HttpServer(_config))
                {
                    var resourceName = resourceLocation + "." + file;
                    var assembly = Assembly.GetExecutingAssembly();
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        var streamContent = new StreamContent(stream);
                        var stringContent = new StringContent("upadhyay.aashish9@yahoo.com");
                        using (var client = new HttpClient(server))
                        {
                            using (var formData = new MultipartFormDataContent())
                            {
                                formData.Add(stringContent, "EMAIL");
                                formData.Add(streamContent, "FILE", file);
                                var response = client.PostAsync("http://localhost/upload", formData).Result;
                                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                            }
                        }
                    }
                }
            }
        }
    }
}
