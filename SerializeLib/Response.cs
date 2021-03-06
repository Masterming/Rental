﻿using System.Collections.Generic;

namespace SerializeLib
{
    /// <summary>
    /// Response object for serialization between client and server.
    /// </summary>
    public class Response
    {
        public List<Car> cars { get; set; }
        public string errorCode { get; set; }

        public Response() { }

        public Response(string error, List<Car> items = null)
        {
            errorCode = error;
            cars = items;
        }
    }
}
