using System;
using System.Text.Json;
using System.Threading;
using WebsocketLib;

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
            Thread workerThread = new Thread(() => Run(id));
            workerThread.Start();
        }

        internal static void Run(int id)
        {
            PromiseMapElement elem = Promisemap.AcquireElement(id);

            string ip = elem.client.Client.RemoteEndPoint.ToString();

            string json = JsonSerializer.Serialize(elem.GetResponse());

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
