using System;
using System.Net.Sockets;
using System.Windows.Media.TextFormatting;
using WebsocketLib;

namespace Clientside
{
    class Client
    {
        public Client(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public string Ip { get; }

        public int Port { get; }

        public string Send(string msg)
        {
            try
            {
                // connect to the server
                TcpClient client = new TcpClient(Ip, Port);

                // send a message to the server
                Lib.Write(client, msg, true);

                // recieve the response from the server
                byte[] bytes = Lib.Read(client);

                // transform the response into readable text
                var tmp = Lib.DecodeBytes(bytes);
                System.Diagnostics.Trace.WriteLine($"sent JSON: {msg}\n");
                System.Diagnostics.Trace.WriteLine($"received JSON: {tmp}\n");
                return tmp;
            }
            catch (SocketException)
            {
                Console.WriteLine("The server refused the connection");
                return "";
            }
        }
    }
}
