using SerializeLib;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Text.Json;
using WebsocketLib;


namespace Clientside
{
    /// <summary>
    /// Interaction logic for Autoauswahl.xaml
    /// </summary>
    public partial class Autoauswahl : Window
    {
        private readonly double taeglicherPreis = 300;
        private List<Car> cars;
        Client client;

        public Autoauswahl()
        {
            InitializeComponent();

            DateTime start = (DateTime)Application.Current.Properties["start"];
            DateTime end = (DateTime)Application.Current.Properties["end"];

            Request req = new Request(start, end);
            string json = JsonSerializer.Serialize(req);
            string tmp = client.Send(json);
            Response res = JsonSerializer.Deserialize<Response>(tmp);

            if (res.errorCode == "ok")
            {
                cars = res.cars;
            }
            else
            {
                MessageBox.Show("Please try again");
            }

            client = (Client)Application.Current.Properties["client"];

            foreach(Car c in cars)
            {
                Marke.Items.Add(c.brand);
                Typ.Items.Add(c.type);
                Kraftstoff.Items.Add(c.fueltype);
            }
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            /*
            Request req = new Request(start, end); 
            string json = JsonSerializer.Serialize(req);
            string tmp = client.Send(json);
            Response res = JsonSerializer.Deserialize<Response>(tmp);
            if (res.errorCode == "ok")
            {
                Application.Current.Properties["cars"] = res.cars;

                Autoauswahl a = new Autoauswahl();
                a.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please try again");
            }
            */
            Bestellung bestellung = new Bestellung(taeglicherPreis);
            bestellung.Show();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
