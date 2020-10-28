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
            string tmp = client.Send(json);
            if (tmp == "")
            {
                MessageBox.Show("Server is currently not availiable");
                return;
            }
            try
            {
                Response res = JsonSerializer.Deserialize<Response>(tmp);
                if (res.errorCode == "OK" && res.cars != null)
                {
                    cars = res.cars;
                    for (int i = 0; i < cars.Count; i++)
                    {
                        if (!Marke.Items.Contains(cars[i].brand))
                            Marke.Items.Add(cars[i].brand);
                        if (!Typ.Items.Contains(cars[i].type))
                            Typ.Items.Add(cars[i].type);
                        if (!Kraftstoff.Items.Contains(cars[i].fueltype))
                            Kraftstoff.Items.Add(cars[i].fueltype);
                        AddStack(i, cars[i]);
                    }
                }
                else
                {
                    MessageBox.Show("Please try again", "Error");
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("ERROR: The Request is not a valid JSON");
            }
        }

        private void AddStack(int i, Car car)
        {
            Image img = new Image
            {
                Source = new BitmapImage(new Uri($"Pictures/{id}.jpg", UriKind.Relative)),
                Width = 94
            };
            TextBlock tb = new TextBlock
            {
                Text = car.model
            };

            StackPanel sp = new StackPanel();
            sp.Children.Add(img);
            sp.Children.Add(tb);

            Button b = new Button
            {
                Name = $"B{id}",
                Content = sp,
                Background = Brushes.Orange,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(3)
            };
            b.SetValue(Grid.RowProperty, (i/4)+1);
            b.SetValue(Grid.ColumnProperty, i % 4);
            b.Click += new RoutedEventHandler(Model_Click);

            Autos.Children.Add(b);
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string name = b.Name.Remove(0, 1);
            int.TryParse(name, out id);

            DetailsMarke.Text = cars[id].brand;
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
