using System.Net.Http.Headers;
using System.Web.Http;
using DocumentUpload;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocumentUploadTests
{
    public class TestBase
    {
        protected HttpConfiguration _config;

        public TestBase()
        {
            _config = new HttpConfiguration();
            _config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            _config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}