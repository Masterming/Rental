using System;
using System.Net.Sockets;
using WebsocketLib;

namespace Clientside
{
    class Program
    {
        public static void Main()
        {
            string ip = "127.0.0.1";
            int port = 80;
            Client client = new Client(ip, port);

            string msg = "test";
            string res = client.Send(msg);
            Console.WriteLine($"Sent: {msg}");
            if(res == null)
                Console.WriteLine($"Received: {res}");
            else
                Console.WriteLine("The server refused the connection");
        }
    }
}
