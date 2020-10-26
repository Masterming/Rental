using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside
{
    class Car
    {
        public readonly int id;
        public readonly string model;
        public readonly string brand;
        public readonly string fueltype;
        public readonly int power;
        public readonly string type;
        public readonly int seats;
        public readonly int doors;
        public readonly int pricePerDay;

        Car(int _id, string _model, string _brand, string _fueltype, int _power, string _type, int _seats, int _doors, int _pricePerDay)
        {
            id          = _id;
            model       = _model;
            brand       = _brand;
            fueltype    = _fueltype;
            power       = _power;
            type        = _type;
            seats       = _seats;
            doors       = _doors;
            pricePerDay = _pricePerDay;
        }
    }
}
