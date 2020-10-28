using System.Net.Sockets;
using SerializeLib;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System;

namespace Serverside
{
    internal static class RequestHandler
    {
        
        public static void Handle(TcpClient client, string json)
        {
            Thread workerThread = new Thread(() => run(client, json));
            workerThread.Start();
        }

        static internal void run(TcpClient client, string json)
        {
            try { 
                Request r = JsonSerializer.Deserialize<Request>(json);
                PromiseMapElement elem = new PromiseMapElement(client, r);
                elem.ToggleState();
                int id = Promisemap.Add(elem);
                SQL_Socket.execute(id);
            }
            catch (JsonException)
            {
                Console.WriteLine("ERROR: The Request is not a valid JSON");
            }
        }
    }
}
