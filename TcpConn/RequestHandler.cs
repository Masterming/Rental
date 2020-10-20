using System;
using System.Net.Sockets;
using WebsocketLib;

namespace Serverside
{
    internal static class RequestHandler
    {
        static RequestHandler()
        {
            Responses = new Responses();
        }

        internal static Responses Responses { get; set; }

        public static void Handle(TcpClient client, string s)
        {
            string res = Responses.ExecuteFunc(s);
            if (res == null)
                res = "Sorry, I didn't quite get that...";

            // Send back a response.
            Lib.Write(client, res, false);
            Console.WriteLine($"Sent: {res}");

            // Disconnect client after sending the response
            client.Close();
            Console.WriteLine("Client disconnected.\n");
        }
    }
}
