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

            Console.Write("Message: ");
            string msg = Console.ReadLine(); ;
            Console.WriteLine($"Sent: {msg}");

            string res = client.Send(msg);
            if(res == null)
                Console.WriteLine("The server refused the connection");
            else
                Console.WriteLine($"Received: {res}");

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }
}
