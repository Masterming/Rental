using SerializeLib;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Clientside
{
    /// <summary>
    /// Interaction logic for Autoauswahl.xaml
    /// </summary>
    public partial class Autoauswahl : Window
    {
        private List<Car> cars;
        private int id = -1;

        public Autoauswahl()
        {
            InitializeComponent();

            DateTime start = (DateTime)Application.Current.Properties["start"];
            DateTime end = (DateTime)Application.Current.Properties["end"];

            Client client = (Client)Application.Current.Properties["client"];
            Request req = new Request(start, end);
            string json = JsonSerializer.Serialize(req);
            System.Diagnostics.Trace.WriteLine($"sent JSON: {json}\n");
            string tmp = client.Send(json);
            System.Diagnostics.Trace.WriteLine($"received JSON: {tmp}\n");
            if(tmp == "")
                MessageBox.Show("Server is currently not availiable");
            else {
                Response res = JsonSerializer.Deserialize<Response>(tmp);

                if (res.errorCode == "OK" && cars != null)
                {
                    cars = res.cars;
                    for (int id = 0; id < cars.Count; id++)
                    {
                        Marke.Items.Add(cars[id].brand);
                        Typ.Items.Add(cars[id].type);
                        Kraftstoff.Items.Add(cars[id].fueltype);
                        AddStack(cars[id].id, cars[id].model);
                    }
                }
                else
                {
                    MessageBox.Show("Please try again", "Error");
                }                
            }
        }

        private void AddStack(int id, string text)
        {
            Image img = new Image
            {
                Source = new BitmapImage(new Uri($"Pictures/{id}.jpg"))
            };
            TextBlock tb = new TextBlock
            {
                Text = text
            };

            StackPanel sp = new StackPanel();
            sp.Children.Add(img);
            sp.Children.Add(tb);

            Button b = new Button
            {
                Name = id.ToString(),
                Content = sp,
                Background = Brushes.Orange,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(3)
            };
            b.Click += new RoutedEventHandler(Model_Click);

            Autos.Children.Add(b);
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int.TryParse(b.Name, out id);

            Marke.Text = cars[id].brand;
            DetailsModell.Text = cars[id].model;
            DetailsLeistung.Text = cars[id].power.ToString();
            DetailsSitzplaetze.Text = cars[id].seats.ToString();
            DetailsKraftstoff.Text = cars[id].fueltype;
            DetailsAntriebsart.Text = cars[id].type;
            Preis.Text = cars[id].pricePerDay.ToString();
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            if(id == -1)
            {
                MessageBox.Show("Please select a car", "Fehlendes Auto");
                return;
            }
            Bestellung bestellung = new Bestellung(cars[id], id);
            bestellung.Show();
            this.Hide();
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow datumsauswahl = new MainWindow();
            datumsauswahl.Show();
            this.Hide();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
