using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using WebsocketLib;

namespace Serverside
{
    class Program
    {
        public static void Main()
        {
            string ip = "127.0.0.1";
            int port = 80;
            Responses responses = new Responses();
            responses.Add("hello server", () => "hello world");
            responses.Add("test", () => "test");

            Server server = new Server(ip, port, responses);
            server.Run();

            // TODO: Find a way to close server
            //Console.WriteLine("Press any key to close.");
            //Console.ReadKey();
            //server.Stop();
        }
    }
}