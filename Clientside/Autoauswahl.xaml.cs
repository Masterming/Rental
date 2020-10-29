using SerializeLib;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Clientside
{
    /// <summary>
    /// Logic for für Autoauswahl.xaml
    /// Manages Picked Car for Rental
    /// </summary>
    public partial class Autoauswahl : Window
    {
        private List<Car> cars;
        private int id = -1;
        private int index;

        public Autoauswahl()
        {
            InitializeComponent();
            UseLayoutRounding = true;

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
                System.Diagnostics.Trace.WriteLine("ERROR: The Request is not a valid JSON");
            }
        }

        private void AddStack(int i, Car car)
        {
            Image img = new Image
            {
                Source = new BitmapImage(new Uri($"Pictures/{car.id}.jpg", UriKind.Relative)),
                Height = 70
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
                Name = $"B{car.id}",
                Content = sp,
                Background = Brushes.Orange,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(3)
            };
            b.SetValue(Grid.RowProperty, (i / 4) + 1);
            b.SetValue(Grid.ColumnProperty, i % 4);
            b.Click += new RoutedEventHandler(Model_Click);

            Autos.Children.Add(b);
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string name = b.Name.Remove(0, 1);
            int.TryParse(name, out id);
            for (index = 0; index < cars.Count; index++)
            {
                if (cars[index].id == id)
                    break;
            }

            DetailsMarke.Text = cars[index].brand;
            DetailsModell.Text = cars[index].model;
            DetailsLeistung.Text = cars[index].power.ToString();
            DetailsSitzplaetze.Text = cars[index].seats.ToString();
            DetailsKraftstoff.Text = cars[index].fueltype;
            DetailsAntriebsart.Text = cars[index].type;
            Preis.Text = cars[index].pricePerDay.ToString();
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            if (id == -1)
            {
                MessageBox.Show("Bitte wähle ein Auto", "Fehlende Auswahl");
                return;
            }
            Bestellung bestellung = new Bestellung(cars[index], cars[index].id);
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

        private void DropdownChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                var color = Brushes.White;
                if (Marke.Text == cars[i].brand || Marke.Text == "Marke")
                {
                    if (Typ.Text == cars[i].type || Typ.Text == "Fahrzeugtyp")
                    {
                        if (Kraftstoff.Text == cars[i].fueltype || Kraftstoff.Text == "Kraftstoff")
                        {
                            color = Brushes.Black;
                        }
                    }
                }
                Button b = (Button)Autos.Children[i];
                b.BorderBrush = color;
            }
        }
    }
}
