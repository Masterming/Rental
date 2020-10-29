namespace SerializeLib
{
    /// <summary>
    /// Data class for serialization between server and client.
    /// </summary>
    public class Car
    {
        public int id { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string fueltype { get; set; }
        public int power { get; set; }
        public string type { get; set; }
        public int seats { get; set; }
        public int doors { get; set; }
        public int pricePerDay { get; set; }

        public Car() { }
        public Car(int _id, string _model, string _brand, string _fueltype, int _power, string _type, int _seats, int _doors, int _pricePerDay)
        {
            id = _id;
            model = _model;
            brand = _brand;
            fueltype = _fueltype;
            power = _power;
            type = _type;
            seats = _seats;
            doors = _doors;
            pricePerDay = _pricePerDay;
        }
    }
}
