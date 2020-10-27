using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebsocketLib;
using SerializeLib;

namespace Serverside
{
    internal static class DeliveryHandler
    {
        //TODO implement handling
        internal static void send(TcpClient client, Response res)
        {
            string ip = client.Client.RemoteEndPoint.ToString();

            string json = JsonSerializer.Serialize(res);

            // Send back a response.
            Lib.Write(client, json, false);
            Console.WriteLine($"({ip}) Sent: {res}");

            // Disconnect client after sending the response
            client.Close();
            Console.WriteLine($"({ip}) Client disconnected");
        }
    }
}
