using System;
using System.Net.Sockets;
using WebsocketLib;

namespace Serverside
{
    internal static class DeliveryHandler
    {
        internal static void Handle(TcpClient client, string res)
        {
            string ip = client.Client.RemoteEndPoint.ToString();

            // Send back a response.
            Lib.Write(client, res, false);
            Console.WriteLine($"({ip}) Sent: {res}");

            // Disconnect client after sending the response
            client.Close();
            Console.WriteLine($"({ip}) Client disconnected");
        }
    }
}
