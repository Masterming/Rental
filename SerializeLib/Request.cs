using Serverside;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside
{
    class Request
    {
        public readonly DateTime start;
        public readonly DateTime end;
        public readonly int carID;

        Request(DateTime _start, DateTime _end, int id = -1)
        {
            start = _start;
            end = _end;
            carID = id;
        }
    }
}