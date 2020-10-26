using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside
{
    class Response
    {
        public readonly List<Car> cars;
        public readonly string errorCode;

        Response(string error, List<Car> items = null)
        {
            errorCode = error;
            cars = items;
        }
    }
}
