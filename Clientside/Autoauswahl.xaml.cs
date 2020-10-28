using System.Windows;

namespace Clientside
{
    /// <summary>
    /// Interaction logic for Autoauswahl.xaml
    /// </summary>
    public partial class Autoauswahl : Window
    {
        private readonly double taeglicherPreis = 300;

        public Autoauswahl()
        {
            InitializeComponent();

            // TODO: get data from server
            // Client client = (Client) Application.Current.Properties["client"];
            // DateTime start = (DateTime)Application.Current.Properties["start"];
            // DateTime end = (DateTime)Application.Current.Properties["end"];
            // client.GetData(start.ToShortDateString(), end.ToShortDateString();

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

        private void Model_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            Bestellung bestellung = new Bestellung(taeglicherPreis);
            bestellung.Show();
            this.Hide();
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow datumsauswahl = new MainWindow();
            datumsauswahl.Show();
            this.Hide();
        }
    }
}
