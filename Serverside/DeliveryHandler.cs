using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebsocketLib;
using SerializeLib;
using System.Threading;
using System.Text;

namespace Serverside
{
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
            send(elem.client, elem.getResponse());
            Promisemap.Remove(id);
        }


        internal static void send(TcpClient client, Response res)
        {
            string ip = client.Client.RemoteEndPoint.ToString();

            string json = JsonSerializer.Serialize(res);

            // Send back a response.
            Lib.Write(client, json, false);
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"({ip}) Sent: {json}");

            // Disconnect client after sending the response
            client.Close();
            Console.WriteLine($"({ip}) Client disconnected");
        }
    }
}
