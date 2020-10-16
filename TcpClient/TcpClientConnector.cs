using System;
using System.Net.Sockets;
using WebsocketLib;

namespace Client
{
    class TcpClientConnector
    {
        public static void Main()
        {
            string ip = "127.0.0.1";
            int port = 80;

            try
            {
                // connect to the server
                TcpClient client = new TcpClient(ip, port);

                // send a message to the server
                string msg = "test";
                Lib.Write(client, msg, true);
                Console.WriteLine("Sent: {0}", msg);

                // recieve the response from the server
                byte[] bytes = Lib.Read(client);

                // transform the response into readable text
                string s = Lib.DecodeBytes(bytes);
                Console.WriteLine("Received: {0}", s);
            }
            catch (SocketException)
            {
                Console.WriteLine("The server refused the connection");
            }
        }
    }
}
