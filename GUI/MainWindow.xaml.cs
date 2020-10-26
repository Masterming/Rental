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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Abholdatum.SelectedDate = DateTime.Today.Date;
            Rückgabedatum.SelectedDate = DateTime.Today.Date;
            Vermietungszeitraum.Text = Abholdatum.SelectedDate.Value.ToShortDateString() + "-" + Rückgabedatum.SelectedDate.Value.ToShortDateString();
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            DateTime Abholdatum = this.Abholdatum.SelectedDate.Value;
            DateTime Rückgabedatum = this.Rückgabedatum.SelectedDate.Value;

            Autoauswahl a = new Autoauswahl(Abholdatum, Rückgabedatum);
            a.Show();
            this.Hide();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            String Startdatum;
            String Enddatum;

            if (
                Abholdatum.SelectedDate != null)
            {
                Startdatum = Abholdatum.SelectedDate.Value.ToShortDateString();
            }
            else
            {
                Startdatum = "";
            }

            if (
             Rückgabedatum.SelectedDate != null)
            {
                Enddatum = Rückgabedatum.SelectedDate.Value.ToShortDateString();
            }
            else
            {
                Enddatum = "";
            }

            Vermietungszeitraum.Text = Startdatum + "-" + Enddatum;
        }
    }
}
