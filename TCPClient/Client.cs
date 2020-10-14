using ServiceLib;
using System;
using System.ServiceModel;

namespace TCPClient
{
    class Client
    {
        public const string Host = "net.tcp://localhost";
        public const int Port = 31227;
        public const string ServiceName = "Service";
        private IService channel;

        public Client()
        {
            try
            {
                // Address
                string svcAddress = Host + ":" + Port + "/" + ServiceName;
                EndpointAddress address = new EndpointAddress(svcAddress);

                // Binding
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message);
                //BasicHttpBinding binding = new BasicHttpBinding();

                // Create Channel
                ChannelFactory<IService> factory = new ChannelFactory<IService>(binding);
                channel = factory.CreateChannel(address);


                Console.WriteLine("Connected to service '" + svcAddress + "'.");

                factory.Close();
            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine("The service operation timed out. " + timeProblem.Message);
                Console.Read();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine("There was a communication problem. " + commProblem.Message);
                Console.Read();
            }
        }

        public IService Channel { get => channel; set => channel = value; }
    }
}
