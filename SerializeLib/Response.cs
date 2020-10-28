using System.Collections.Generic;

namespace SerializeLib
{
    /// <summary>
    /// Response object for serialization between client and server.
    /// </summary>
    public class Response
    {
        public readonly List<Car> cars;
        public readonly string errorCode;

        public Response(string error, List<Car> items = null)
        {
            errorCode = error;
            cars = items;
        }
    }
}
