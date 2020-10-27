using SerializeLib;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Threading;

namespace Serverside
{
    static class SQL_Socket
    {
        private static readonly SqliteConnection db;
        private static Mutex dbMutex;

        static internal void execute(int id)
        {
            Thread workerThread = new Thread(() => run(id));
            workerThread.Start();
        }
        static internal void run(int id)
        {
            PromiseMapElement elem = Promisemap.AcquireElement(id);
            Request rq = elem.request;
        }
    }
}