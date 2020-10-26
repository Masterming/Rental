using System.Net.Sockets;
using SerializeLib;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Serverside
{
    internal static class RequestHandler
    {
        
        public static void Handle(TcpClient client, string json)
        {
            ThreadPool.QueueUserWorkItem(run(client, json));
        }

        static internal void run(TcpClient client, string json)
        {
            Request r = JsonSerializer.Deserialize<Request>(json);
            PromiseMapElement elem = new PromiseMapElement(client, r);
            elem.ToggleState();
            int id = Promisemap.Add(elem);
            //TODO run SQL Socket
        }
    }
}
