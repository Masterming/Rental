using Microsoft.Data.Sqlite;
using SerializeLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Serverside
{
    /// <summary>
    /// Implements SQLite connection.
    /// Forms and executes SQL statements.
    /// Generates serializable Response Object.
    /// </summary>
    static class SQL_Socket
    {
        private static SqliteConnection db;
        private static Mutex dbMutex = new Mutex();
        private static bool init = false;
        private static uint vermietungID = 0;

        public static void Execute(int id)
        {
            Thread workerThread = new Thread(() => Run(id));
            workerThread.Start();
        }

        static internal void Run(int id)
        {
            PromiseMapElement elem = Promisemap.AcquireElement(id);
            try
            {
                Request rq = elem.request;
                List<Car> cars = new List<Car>();
                bool valid = true;

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

                    try
                    {
                        SqliteCommand cmd = db.CreateCommand();
                        cmd.CommandText =
                            @"Select VermietungID FROM Vermietung ORDER BY VermietungID DESC LIMIT 1";
                        SqliteDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {
                            r.Read();
                            vermietungID = (uint)r.GetInt32(0) + 1;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    db.Open();
                }

                SqliteCommand command = db.CreateCommand();

                if (rq.carID == -1)
                {
                    command.CommandText =
                    @"
                    SELECT *
                    FROM Autos
                    WHERE AutoID NOT IN(
                        SELECT AutoID
                        FROM Vermietung
                        WHERE(Anfang <= $endDate  AND Anfang >= $startDate)
                        OR   (Ende   <= $endDate  AND Ende   >= $startDate)
                        )
                    ";
                    command.Parameters.AddWithValue("$startDate", startDate);
                    command.Parameters.AddWithValue("$endDate", endDate);
                }
                else
                {
                    try
                    {
                        SqliteCommand cmd = db.CreateCommand();
                        cmd.CommandText =
                        @"
                        SELECT AutoID
                        FROM Vermietung
                        WHERE(AutoID = $id
                        AND ((Anfang <= $endDate  AND Anfang >= $startDate)
                        OR   (Ende   <= $endDate  AND Ende   >= $startDate))
                        )
                        ";
                        cmd.Parameters.AddWithValue("$id", elem.request.carID);
                        cmd.Parameters.AddWithValue("$startDate", startDate);
                        cmd.Parameters.AddWithValue("$endDate", endDate);
                        SqliteDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {
                            r.Read();
                            valid = false;
                            Console.WriteLine($"Booking Validity Check returned: {r.GetInt32(0)}");
                            r.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Booking Validity Check returned:");
                        Console.WriteLine(e.Message);
                    }

                    if (valid)
                    {
                        command.CommandText =
                        @"INSERT INTO Vermietung (VermietungID, Anfang, Ende, AutoID)
                        VALUES($vermietungID, $startDate, $endDate, $id)                        
                        ";
                        command.Parameters.AddWithValue("$vermietungID", vermietungID++);
                        command.Parameters.AddWithValue("$startDate", startDate);
                        command.Parameters.AddWithValue("$endDate", endDate);
                        command.Parameters.AddWithValue("$id", elem.request.carID);
                    }
                }

                if (valid)
                {
                    SqliteDataReader reader = command.ExecuteReader();

                    IEnumerator readerEnumerator = reader.GetEnumerator();

                    while (readerEnumerator.MoveNext())
                    {
                        //AutoID int NOT NULL PRIMARY KEY, Modell char(50), Marke char(50),
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

                        cars.Add(new Car(_AutoID, _model, _brand, _fueltype, _power, _type, _seats, _doors, _pricePerDay));
                    }
                }

                dbMutex.ReleaseMutex();

                db.Close();

                if (valid)
                {
                    elem.SetResponse(new Response("OK", cars));
                }
                else
                {
                    elem.SetResponse(new Response("INVALID"));
                }
                elem.ToggleState();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occured: {e.Message}");
                elem.SetResponse(new Response(e.Message));
            }

            Promisemap.ReleaseElement(elem);

            DeliveryHandler.Handle(id); //Forward to DeliveryHandler
        }


        public static void InitializeDatabase(string source)
        {
            System.Diagnostics.Trace.WriteLine("Creating Database");
            SqliteCommand command = db.CreateCommand();
            string strCommand = File.ReadAllText(source);
            command.CommandText = strCommand;
            command.ExecuteNonQuery();
        }
    }
}