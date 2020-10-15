using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using WebsocketLib;

namespace Server
{
    class TcpServer
    {
        public static void Main()
        {
            string ip = "127.0.0.1";
            int port = 80;
            TcpListener server = null;

            try
            {
                // start the server
                server = new TcpListener(IPAddress.Parse(ip), port);
                server.Start();
                Console.WriteLine("Server has started on {0}:{1}", ip, port);

                // Wait for a connection request from a client
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...\n");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    // Loop to catch all messages sent by the client
                    while (true)
                    {
                        byte[] bytes = Lib.Read(client);
                        string s = Encoding.UTF8.GetString(bytes);

                        // Handshake between Client and Server
                        if (Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase))
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
                            var stream = client.GetStream();
                            stream.Write(res, 0, res.Length);
                        }
                        // Transform message into readable Encoding
                        else
                        {
                            s = Lib.DecodeBytes(bytes);
                            Console.WriteLine("Received: {0}", s);

                            string res = "";
                            res = s switch
                            {
                                "hallo server" => "hallo user",
                                "test" => "test",
                                _ => "0",
                            };

                            // Send back a response.
                            Lib.Write(client, res, false);
                            Console.WriteLine("Sent: {0}", res);

                            // Disconnect client after sending the response
                            client.Close();
                            Console.WriteLine("Client disconnected.\n");
                            break;
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}