using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebsocketLib;
using SerializeLib;
using System.Threading;

namespace Serverside
{
    /// <summary>
    /// Serializes, sends, and dequeues Response.
    /// Closes Connection to client.
    /// </summary>
    internal static class DeliveryHandler
    {
        public static void Handle(int id)
        {
            Thread workerThread = new Thread(() => run(id));
            workerThread.Start();
        }

        internal static void run(int id)
        {
            PromiseMapElement elem = Promisemap.AcquireElement(id);

            string ip = elem.client.Client.RemoteEndPoint.ToString();

            string json = JsonSerializer.Serialize(elem.getResponse());

            // Send back a response.
            Lib.Write(elem.client, json, false);
            Console.WriteLine($"({ip}) Sent: {json}");

            // Disconnect client after sending the response
            elem.client.Close();
            Console.WriteLine($"({ip}) Client disconnected");

            Promisemap.Remove(id);
        }
    }
}
