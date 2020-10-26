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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
