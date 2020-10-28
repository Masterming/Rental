using System;
using System.Windows;
using SerializeLib;
using System.Text.Json;

namespace Clientside
{
    /// <summary>
    /// Interaction logic for Bestellung.xaml
    /// </summary>
    public partial class Bestellung : Window
    {
        int id;

        public Bestellung(Car car, int id)
        {
            InitializeComponent();

            this.id = id;
            DateTime start = (DateTime)Application.Current.Properties["start"];
            DateTime end = (DateTime)Application.Current.Properties["end"];

            Marke.Text = car.brand;
            Modell.Text = car.model;
            Leistung.Text = car.power.ToString();
            Sitzplaetze.Text = car.seats.ToString();
            Kraftstoff.Text = car.fueltype;
            Antriebsart.Text = car.type;
            Preis.Text = (((end - start).TotalDays + 1) * car.pricePerDay).ToString();
            Vermietungszeitraum.Text = car.brand;
        }

        private void Bestellen_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = (DateTime)Application.Current.Properties["start"];
            DateTime end = (DateTime)Application.Current.Properties["end"];
            Client client = (Client)Application.Current.Properties["client"];

            Request req = new Request(start, end, id);
            string json = JsonSerializer.Serialize(req);
            string tmp = client.Send(json);
            Response res = JsonSerializer.Deserialize<Response>(tmp);
            if (res.errorCode == "ok")
            {
                bestellen.IsEnabled = false;
                MessageBoxResult result = MessageBox.Show("Auto erfolgreich gemietet. Beenden?", "Success", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Please try again");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Autoauswahl_Click(object sender, RoutedEventArgs e)
        {
            Autoauswahl autoauswahl = new Autoauswahl();
            autoauswahl.Show();
            this.Hide();
        }
    }
}
