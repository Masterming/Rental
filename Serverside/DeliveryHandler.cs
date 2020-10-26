using SerializeLib;
using System;
using System.Net.Sockets;
using WebsocketLib;

namespace Serverside
{
    internal static class DeliveryHandler
    {
        //TODO implement handling
        internal static void Send(TcpClient client, Response res)
        {
            string ip = client.Client.RemoteEndPoint.ToString();

            string json = ""; //TODO serialization

            // Send back a response.
            Lib.Write(client, json, false);
            Console.WriteLine($"({ip}) Sent: {res}");

            // Disconnect client after sending the response
            client.Close();
            Console.WriteLine($"({ip}) Client disconnected");
        }
    }
}
