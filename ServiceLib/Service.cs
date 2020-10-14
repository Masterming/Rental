using CarLib;
using System;
using System.Collections.Generic;

namespace ServiceLib
{
    public class Service : IService
    {
        static readonly List<PKW> Autos = new List<PKW>();
        static int AutoAnzahl = 0;

        public int GetAA()
        {
            return AutoAnzahl;
        }

        public List<PKW> GetAutos()
        {
            return Autos;
        }

        public bool AddAuto(PKW _Auto)
        {
            try
            {
                Autos.Add(_Auto);
                AutoAnzahl++;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
