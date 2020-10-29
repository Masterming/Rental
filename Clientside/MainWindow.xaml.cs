using System;
using System.Windows;
using System.Windows.Controls;

namespace Clientside
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Client client;
        public MainWindow()
        {
            InitializeComponent();
            SelectedDateChanged(null, null);
            if(client == null) {
                client = new Client("127.0.0.1", 80);
                Application.Current.Properties["client"] = client;
            }
        }
        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            Autoauswahl a = new Autoauswahl();
            a.Show();
            this.Hide();
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Abholdatum.SelectedDate == null)
                Abholdatum.SelectedDate = DateTime.Today.Date;

            if (Rückgabedatum.SelectedDate == null)
                Rückgabedatum.SelectedDate = DateTime.Today.Date;

            DateTime start = Abholdatum.SelectedDate.Value;
            DateTime end = Rückgabedatum.SelectedDate.Value;
            Application.Current.Properties["start"] = start;
            Application.Current.Properties["end"] = end;
            Vermietungszeitraum.Text = $"{start.ToShortDateString()} - {end.ToShortDateString()}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
