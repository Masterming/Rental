using ServiceLib;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace TCPServer
{
    class Server
    {
        public const string Host = "net.tcp://localhost";
        public const int Port = 31227;
        public const string ServiceName = "Service";

        static void Main()
        {
            // Address
            string svcAddress = Host + ":" + Port + "/" + ServiceName;
            Uri svcUri = new Uri(svcAddress);

            using (ServiceHost sh = new ServiceHost(typeof(Service), svcUri))
            {
                // Binding
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message);
                // BasicHttpBinding binding = new BasicHttpBinding();

                // Behavior
                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                // behavior.HttpGetEnabled = true;
                sh.Description.Behaviors.Add(behavior);

                //Endpoint
                //sh.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                sh.AddServiceEndpoint(typeof(IService), binding, svcAddress);

                // Connection
                sh.Open();

                Console.WriteLine("Service started '" + svcAddress + "' ...Press any key to quit.");
                Console.ReadKey();

                sh.Close();
            }
        }
    }
}
