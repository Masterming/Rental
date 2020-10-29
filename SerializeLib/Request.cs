using System;

namespace SerializeLib
{
    /// <summary>
    /// Request object for Serialization between client and server.
    /// </summary>
    public class Request
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int carID { get; set; }

        public Request() { }

        public Request(DateTime _start, DateTime _end, int id = -1)
        {
            start = _start;
            end = _end;
            carID = id;
        }
    }
}