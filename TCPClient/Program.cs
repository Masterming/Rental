using CarLib;
using System;
using System.Collections.Generic;

namespace TCPClient
{
    class Program
    {
        static void Main()
        {
            Client c = new Client();

            // Test
            PKW Auto1 = new PKW();

            Console.WriteLine("Model: ");
            Auto1.Model = Console.ReadLine();
            Console.WriteLine("Colour: ");
            Auto1.Farbe = Console.ReadLine();
            Console.WriteLine("PSn: ");
            Auto1.PS = Convert.ToInt16(Console.ReadLine());

            if (c.Channel.AddAuto(Auto1))
            {
                Console.WriteLine("inserted");
            }
            else
            {
                Console.WriteLine("error");
            }

            //Sauber über Listenzeiger! (Liste wird dabei dem Client "serialisiert" übergeben!!)
            List<PKW> cars;
            cars = c.Channel.GetAutos();

            Console.WriteLine("count: " + c.Channel.GetAA());
            Console.WriteLine("model: " + cars[0].Model);
            Console.WriteLine("colour: " + cars[0].Farbe);
            Console.WriteLine("PS: " + cars[0].PS);

            Console.WriteLine("Press key to quit.");
            Console.ReadKey();


        }
    }
}
