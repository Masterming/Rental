using System;
using System.Windows;

namespace Clientside
{
    /// <summary>
    /// Interaction logic for Bestellung.xaml
    /// </summary>
    public partial class Bestellung : Window
    {
        private readonly double taeglicherPreis;
        private readonly double gesamt;

        public Bestellung(double taeglicherPreis)
        {
            InitializeComponent();

            DateTime start = (DateTime)Application.Current.Properties["start"];
            DateTime end = (DateTime)Application.Current.Properties["end"];
            this.taeglicherPreis = taeglicherPreis;
            this.gesamt = ((end - start).TotalDays + 1) * taeglicherPreis;
            //MessageBox.Show("Gesamtpreis: " + gesamt);
        }

        private void Bestellen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Autoauswahl_Click(object sender, RoutedEventArgs e)
        {
            Autoauswahl autoauswahl = new Autoauswahl();
            autoauswahl.Show();
            this.Hide();
        }
    }
}
