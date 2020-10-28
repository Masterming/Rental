using SerializeLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Threading;
using System.Collections;

namespace Serverside
{
    static class SQL_Socket
    {
        private static SqliteConnection db;
        private static Mutex dbMutex = new Mutex();
        private static bool init = false;
        private static uint vermietungID = 20;

        public static void execute(int id)
        {
            Thread workerThread = new Thread(() => run(id));
            workerThread.Start();
        }

        static internal void run(int id)
        {
            PromiseMapElement elem = Promisemap.AcquireElement(id);
            try
            {
                Request rq = elem.request;
                List<Car> cars = new List<Car>();

                string startDate = rq.start.Year.ToString() + rq.start.Month.ToString() + rq.start.Day.ToString();
                string endDate = rq.end.Year.ToString() + rq.end.Month.ToString() + rq.end.Day.ToString();
                //yyyymmdd

                dbMutex.WaitOne(-1);

                if (!init)
                {
                    init = true;
                    string source = $"..\\..\\..\\..\\Datenbank\\Datenbank.sql";
                    string destination = $"..\\..\\..\\..\\Datenbank\\Datenbank.db";

                    bool tmp = false;
                    if (!File.Exists(destination))
                        tmp = true;

                    db = new SqliteConnection("Data Source=" + destination);
                    db.Open();

                    if (tmp)
                        InitializeDatabase(source);
                }

                SqliteCommand command = db.CreateCommand();

                if (rq.carID == -1)
                {
                    command.CommandText =
                    @"
                    SELECT *
                    FROM Autos";
                    //WHERE NOT EXISTS(
                    //    SELECT 1
                    //    FROM Vermietung
                    //    WHERE (Anfang <= $endDate AND Anfang >= $startDate)
                    //    OR (Ende >= $startDate AND Ende <= $endDate)
                    //    )
                    //";
                    //command.Parameters.AddWithValue("$startDate", startDate);
                    //command.Parameters.AddWithValue("$endDate", endDate);
                }
                else
                {
                    command.CommandText =
                        @"
                        INSERT INTO Vermietung (VermietungID, Anfang, Ende, AutoID)
                        VALUES($vermietungID, $startDate, $endDate, $id)
                        WHERE NOT EXISTS(
                            SELECT 1
                            FROM Vermietung
                            WHERE (Anfang <= $endDate AND >= $startDate)
                            OR (Ende >= $startDate AND Ende <= $endDate)
                        )
                        ";
                    command.Parameters.AddWithValue("$vermietungID", vermietungID++);
                    command.Parameters.AddWithValue("$startDate", startDate);
                    command.Parameters.AddWithValue("$endDate", endDate);
                    command.Parameters.AddWithValue("$id", id);
                }

                SqliteDataReader reader = command.ExecuteReader();

                IEnumerator readerEnumerator = reader.GetEnumerator();

                while (readerEnumerator.MoveNext())
                {
                    //AutoID int AUTO_INCREMENT NOT NULL PRIMARY KEY, Modell char(50), Marke char(50),
                    //Kraftstoffart char(50), Leistung int, Typ char(50), Sitzplaetze int, Tueren int, Tagespreis int
                    int _AutoID = reader.GetInt32(0);
                    string _model = reader.GetString(1);
                    string _brand = reader.GetString(2);
                    string _fueltype = reader.GetString(3);
                    int _power = reader.GetInt32(4);
                    string _type = reader.GetString(5);
                    int _seats = reader.GetInt32(6);
                    int _doors = reader.GetInt32(7);
                    int _pricePerDay = reader.GetInt32(8);

                    Console.WriteLine($"\tCar: {_AutoID}, {_model}, {_brand}, {_fueltype}, {_power}, {_type}, {_seats}, {_doors}, {_pricePerDay}");

                    cars.Add(new Car(_AutoID, _model, _brand, _fueltype, _power, _type, _seats, _doors, _pricePerDay));
                }

                dbMutex.ReleaseMutex();

                db.Close();

                elem.setResponse(new Response("OK", cars));
            }
            catch(Exception e)
            {
                elem.setResponse(new Response(e.Message));
            }
            Promisemap.ReleaseElement(elem);

            DeliveryHandler.Handle(id);
        }


        public static void InitializeDatabase(string source)
        {
            System.Diagnostics.Trace.WriteLine("Creating Database"); //?
            SqliteCommand command = db.CreateCommand();
            string strCommand = File.ReadAllText(source);
            command.CommandText = strCommand;
            command.ExecuteNonQuery();
        }
    }
}