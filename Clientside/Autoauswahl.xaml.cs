using SerializeLib;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Clientside
{
    /// <summary>
    /// Interaction logic for Autoauswahl.xaml
    /// </summary>
    public partial class Autoauswahl : Window
    {
        private List<Car> cars;
        int id = -1;

        public Autoauswahl()
        {
            InitializeComponent();

            DateTime start = (DateTime)Application.Current.Properties["start"];
            DateTime end = (DateTime)Application.Current.Properties["end"];

            Client client = (Client)Application.Current.Properties["client"];
            Request req = new Request(start, end);
            string json = JsonSerializer.Serialize(req);
            string tmp = client.Send(json);
            if(tmp == "")
                MessageBox.Show("Server is currently not availiable");
            else {
                Response res = JsonSerializer.Deserialize<Response>(tmp);

                if (res.errorCode == "ok")
                {
                    cars = res.cars;
                }
                else
                {
                    MessageBox.Show("Please try again");
                }

                for(int id = 0; id < cars.Count; id++)
                {
                    Marke.Items.Add(cars[id].brand);
                    Typ.Items.Add(cars[id].type);
                    Kraftstoff.Items.Add(cars[id].fueltype);
                    AddStack(cars[id].id, cars[id].model);
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
                Content = sp
            };
            b.Click += new RoutedEventHandler(Model_Click);

            Autos.Children.Add(b);
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            if(id == -1)
            {
                MessageBox.Show("Please select a car");
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
