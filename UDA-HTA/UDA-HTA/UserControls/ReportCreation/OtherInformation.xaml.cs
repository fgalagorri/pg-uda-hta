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
        private List<ExampleEsfuerzo> lstEsfuerzo;
        private List<ExampleComplicacion> lstComplicacion;

        public OtherInformation()
        {
            InitializeComponent();
            lstEsfuerzo = new List<ExampleEsfuerzo>();
            lstComplicacion = new List<ExampleComplicacion>();
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
                lstEsfuerzo.Add(new ExampleEsfuerzo
                    {
                        Hora = hora.ToString("D2") + ":" + min.ToString("D2"),
                        Tipo = cmbTipoEsfuerzo.Text
                    });
            }
            //TODO show error message when the time is not correct

            txtHoraEsfuerzo.Clear();
            txtMinEsfuerzo.Clear();
            cmbTipoEsfuerzo.SelectedIndex = -1;
            grEsfuerzo.DataContext = lstEsfuerzo;
        }

        private void btnAddCompl_Click(object sender, RoutedEventArgs e)
        {
            grComplicaciones.DataContext = null;

            int hora, min;
            string tipo;
            if (int.TryParse(txtHoraComp.Text, out hora)
                && int.TryParse(txtMinComp.Text, out min)
                && 0 <= hora && hora < 24 && 0 <= min && min < 60)
            {
                tipo = cmbTipoComp.Text;
                if (cmbTipoComp.Text.Equals("Otros"))
                    tipo += ": " + txtComplOther;

                lstComplicacion.Add(new ExampleComplicacion
                    {
                        Hora = hora.ToString("D2") + ":" + min.ToString("D2"),
                        Tipo = tipo
                    });
            }
            //TODO show error message when the time is not correct

            txtHoraComp.Clear();
            txtMinComp.Clear();
            cmbTipoComp.SelectedIndex = -1;
            txtComplOther.Clear();
            txtComplOther.IsEnabled = false;
            grComplicaciones.DataContext = lstComplicacion;
        }

        private void cmbTipoComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtComplOther.IsEnabled = ((ComboBoxItem)e.AddedItems).Content.Equals("Otros");
        }
    }

    public class ExampleEsfuerzo
    {
        public string Hora { get; set; }
        public string Tipo { get; set; }
    }
    public class ExampleComplicacion
    {
        public string Hora { get; set; }
        public string Tipo { get; set; }
    }
}
