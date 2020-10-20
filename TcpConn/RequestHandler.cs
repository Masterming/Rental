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

        public static void Handle(TcpClient client, string s, string ip)
        {
            string res = Responses.ExecuteFunc(s);
            if (res == null)
                res = "Failed to recognize request";

            // Send back a response.
            Lib.Write(client, res, false);
            Console.WriteLine($"({ip}) Sent: {res}");

            // Disconnect client after sending the response
            client.Close();
            Console.WriteLine($"({ip}) Client disconnected");
        }
    }
}
