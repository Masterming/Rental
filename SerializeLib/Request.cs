using System;

namespace SerializeLib
{
    /// <summary>
    /// Request object for Serialization between client and server.
    /// </summary>
    public class Request
    {
        public readonly DateTime start;
        public readonly DateTime end;
        public readonly int carID;

        public Request(DateTime _start, DateTime _end, int id = -1)
        {
            start = _start;
            end = _end;
            carID = id;
        }
    }
}