using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using WebsocketLib;

namespace Serverside
{
    static class ClientHandler
    {
        public static void Handle(TcpClient client)
        {
            string ip = client.Client.RemoteEndPoint.ToString();
            Console.WriteLine($"Connected: {ip}");

            byte[] bytes = Lib.Read(client);
            string s = Encoding.UTF8.GetString(bytes);

            // Handshake between Client and Server
            if (Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase))
            {
                byte[] hs = CreateHandshake(s);
                Lib.Write(client, hs);
                Lib.Read(client);
            }

            s = Lib.DecodeBytes(bytes);
            Console.WriteLine($"Recieved ({ip}): {s}");
            RequestHandler.Handle(client, s, ip);
        }
        private static byte[] CreateHandshake(string s)
        {
            string swk = Regex.Match(s, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
            string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
            string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);
            byte[] res = Encoding.UTF8.GetBytes(
                "HTTP/1.1 101 Switching Protocols\r\n" +
                "Connection: Upgrade\r\n" +
                "Upgrade: websocket\r\n" +
                "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");
            return res;
        }
    }
}
