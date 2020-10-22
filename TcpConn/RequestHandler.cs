using System.Net.Sockets;

namespace Serverside
{
    internal static class RequestHandler
    {
        static RequestHandler()
        {
            //Responses = new Responses();
        }

        //internal static Responses Responses { get; set; }

        public static void Handle(TcpClient client, string s)
        {
            PromiseMapElement elem = new PromiseMapElement(client, s);
            Promisemap.Add(elem);
            /*string res = Responses.ExecuteFunc(s);
            if (res == null)
                res = "Failed to recognize request";

            DeliveryHandler.Handle(client, res);*/
        }
    }
}
