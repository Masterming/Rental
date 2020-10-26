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
    /// <summary>
    /// Interaktionslogik für Bestellung.xaml
    /// </summary>
    public partial class Bestellung : Window
    {
        private readonly DateTime sDatum;
        private readonly DateTime eDatum;
        private readonly double taeglicherPreis;
        private readonly double gesamt;

        public Bestellung(DateTime sDatum, DateTime eDatum, double taeglicherPreis)
        {
            InitializeComponent();
            this.sDatum = sDatum;
            this.eDatum = eDatum;
            this.taeglicherPreis = taeglicherPreis;
            this.gesamt = ((eDatum - sDatum).TotalDays + 1) * taeglicherPreis;
            //MessageBox.Show("Gesamtpreis: " + gesamt);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
