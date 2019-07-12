using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DocumentUpload
{
    public class BrokerFactory
    {
        private readonly DocumentBroker _documentBroker;
        private readonly FileRepositoryBroker _fileRepositoryBroker;
        private readonly IInvoiceBroker _invoiceBroker;

        private static BrokerFactory _brokerFactory = new BrokerFactory();

        private BrokerFactory()
        {
            _fileRepositoryBroker = new FileRepositoryBroker();
            _documentBroker = new DocumentBroker();
            _invoiceBroker = new InvoiceBroker();
        }

        public static BrokerFactory Instance => _brokerFactory;

        public IFileRepositoryBroker FileRepositoryBroker => _fileRepositoryBroker;

        public IDocumentBroker DocumentBroker => _documentBroker;

        public IInvoiceBroker InvoiceBroker => _invoiceBroker;
    }
}
