using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    public partial class Autoauswahl : Window
    {
        private DateTime sDatum;
        private DateTime eDatum;
        private double taeglicherPreis = 300;

        public Autoauswahl(DateTime Startdatum, DateTime Enddatum)
        {
            InitializeComponent();

            this.sDatum = Startdatum;
            this.eDatum = Enddatum;

            Marke.Items.Add("BMW");
            Marke.Items.Add("Mercedes");
            Marke.Items.Add("Ferarri");
            Marke.Items.Add("Audi");
            Marke.Items.Add("Porsche");
            Marke.Items.Add("VW");
            Marke.Items.Add("Jeep");

            Typ.Items.Add("Kompaktwagen");
            Typ.Items.Add("Sport");
            Typ.Items.Add("SUV");
            Typ.Items.Add("Coupe");
            Typ.Items.Add("Gelände");
            Typ.Items.Add("Limousine");

            Kraftstoff.Items.Add("Elektro");
            Kraftstoff.Items.Add("Diesel");
            Kraftstoff.Items.Add("Benzin");
        }

        private void M8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Passat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Spyder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GWagon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cherokee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GTI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LaFerrari_Click(object sender, RoutedEventArgs e)
        {

        }

        private void X6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void M4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RS3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void A3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Bestellung bestellung = new Bestellung(sDatum, eDatum, taeglicherPreis);
            bestellung.Show();
        }


    }
}
