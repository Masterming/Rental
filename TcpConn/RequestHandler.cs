using System.Net.Sockets;
using SerializeLib;

namespace Serverside
{
    internal static class RequestHandler
    {
        static RequestHandler()
        {
        }

        public static void Handle(TcpClient client, string s)
        {
            //TODO deserialization
            Request r = null;
            PromiseMapElement elem = new PromiseMapElement(client, r);
            elem.ToggleState();
            int id = Promisemap.Add(elem);
            //TODO run SQL Socket
        }
    }
}
