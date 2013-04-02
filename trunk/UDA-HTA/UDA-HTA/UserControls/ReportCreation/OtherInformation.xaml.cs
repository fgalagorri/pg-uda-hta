using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for OtherInformation.xaml
    /// </summary>
    public partial class OtherInformation : UserControl
    {
        private List<Esfuerzo> lstEsfuerzo;

        public OtherInformation()
        {
            InitializeComponent();
            lstEsfuerzo = new List<Esfuerzo>();
        }

        private void btnAddMedicamento_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddEsfuerzo_Click(object sender, RoutedEventArgs e)
        {
            grEsfuerzo.DataContext = null;

            int hora, min;
            if (int.TryParse(txtHoraEsfuerzo.Text, out hora)
                && int.TryParse(txtMinEsfuerzo.Text, out min)
                && 0 <= hora && hora < 24 && 0 <= min && min < 60)
            {
                lstEsfuerzo.Add(new Esfuerzo
                    {
                        Hora = hora + ":" + min,
                        Tipo = cmbTipoEsfuerzo.Text
                    });
                grEsfuerzo.DataContext = lstEsfuerzo;
            }
            //TODO show error message when the time is not correct

            txtHoraEsfuerzo.Clear();
            txtMinEsfuerzo.Clear();
            cmbTipoEsfuerzo.SelectedIndex = -1;
        }

        private void btnAddCompl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbTipoComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class Esfuerzo
    {
        public string Hora { get; set; }
        public string Tipo { get; set; }
    }
}
