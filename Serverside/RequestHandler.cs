using SerializeLib;
using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace Serverside
{
    /// <summary>
    /// Deserializes JSON request and creates PromiseMapElement.
    /// Forwards to SQL-Socket.
    /// </summary>
    internal static class RequestHandler
    {

        public static void Handle(TcpClient client, string json)
        {
            Thread workerThread = new Thread(() => Run(client, json));
            workerThread.Start();
        }

        static internal void Run(TcpClient client, string json)
        {
            try
            {
                Request r = JsonSerializer.Deserialize<Request>(json);
                PromiseMapElement elem = new PromiseMapElement(client, r);
                elem.ToggleState();
                int id = Promisemap.Add(elem);
                SQL_Socket.Execute(id);
            }
            catch (JsonException)
            {
                Console.WriteLine("ERROR: The Request is not a valid JSON");
            }
        }
    }
}
