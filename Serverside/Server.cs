using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Serverside
{
    /// <summary>
    /// Server class waiting on client requests.
    /// Forwards to ClientHandler.
    /// </summary>
    class Server
    {
        private bool exit;
        private readonly TcpListener server;

        public Server(string ip, int port)
        {
            Ip = ip;
            Port = port;
            server = new TcpListener(IPAddress.Parse(ip), port);
        }

        public string Ip { get; }

        public int Port { get; }

        public void Run()
        {
            try
            {
                server.Start();
                Console.WriteLine("Server has started on {0}:{1}", Ip, Port);
                var task = Task.Run(() => AcceptClientsAsync());
                if (task.IsFaulted)
                    task.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                server.Stop();
            }
        }

        public void Stop()
        {
            exit = true;
            //wait 10s so all threads in the system can exit safely.
            Thread.Sleep(10000);
        }

        async Task AcceptClientsAsync()
        {
            while (!exit)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Task task = Task.Run(() => ClientHandler.Handle(client));
            }
            server.Stop();
        }
    }
}
