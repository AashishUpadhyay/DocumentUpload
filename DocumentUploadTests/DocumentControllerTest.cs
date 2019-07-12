using System;
using System.Net;
using DocumentUpload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Owin.Hosting;

namespace DocumentUploadTests
{
    [TestClass]
    public class DocumentControllerTest : TestBase
    {

        [TestInitialize]
        public void Init()
        {
            var documentController = new DocumentController();
        }

        [TestMethod]
        public void Get()
        {
            using (var server = new HttpServer(_config))
            {
                var client = new HttpClient(server);
                string url = "http://localhost/document/7c5c6f52-af7f-4cca-8e15-b516ff9ca925";
                using (var response = client.GetAsync(url).Result)
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }

        [TestMethod]
        public void GetAll()
        {
            using (var server = new HttpServer(_config))
            {
                var client = new HttpClient(server);
                string url = "http://localhost/document/";
                using (var response = client.GetAsync(url).Result)
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
    }
}
