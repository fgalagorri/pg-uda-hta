using System;
using System.Collections;
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
using System.Windows.Shapes;
using Gateway;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for AddReportsToResearch.xaml
    /// </summary>
    public partial class AddReportsToResearch : Window
    {
        private int _idInvestigation;

        public AddReportsToResearch(int idInvestigation)
        {
            _idInvestigation = idInvestigation;

            InitializeComponent();

            buttonAdd.IsEnabled = false;
        }


        private void enterAge(object sender, KeyEventArgs e)
        {
//            this.btnSearch_Click(sender, e);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int? lowerAge = null;
            int? upperAge = null;

            if (txtEdad.Text.Length > 0)
            {
                //parsear filtro edad
                string text = txtEdad.Text.Trim();
                char firstChar = text[0];
                char[] destination = new char[2];

                string[] ageBetween;
                
                switch (firstChar)
                {
                    case '>':
                        text.CopyTo(1,destination,0,text.Length-1);
                        lowerAge = int.Parse(new string(destination));
                        break;
                    case '<':
                        text.CopyTo(1,destination,0,text.Length-1);
                        upperAge = int.Parse(destination.ToString());
                        break;
                    case '=':
                        text.CopyTo(1,destination,0,text.Length-1);
                        lowerAge = int.Parse(destination.ToString());
                        upperAge = int.Parse(destination.ToString());
                        break;
                    default:
                        if (firstChar >= 0 || firstChar <= 9)
                        {
                            //El primer caracter es un numero, verificar si hay un '-'
                            ageBetween = txtEdad.Text.Trim().Split('-');

                            if (ageBetween.Length == 1)
                            {
                                //No se indico signo, y hay solo un numero, se asumen edades mayores a la indicada
                                lowerAge = int.Parse(ageBetween[0]);
                            }
                            else
                            {
                                lowerAge = int.Parse(ageBetween[0]);
                                upperAge = int.Parse(ageBetween[1]);
                            }
                        }
                        else
                        {
                            // Error
                            MessageBoxResult result = MessageBox.Show("Formato en campo edad incorrecto");
                        }

                        break;
                }
            }

            bool? isSomker = smoker.IsChecked;
            bool? isDiabetic = diabetic.IsChecked;
            bool? isHypertense = hypertense.IsChecked;
            bool? isDyslipidemic = dislipidemic.IsChecked;

            DateTime? sinceDate = null;
            if (dtSinceDate.SelectedDate != null)
            {
                sinceDate = dtSinceDate.SelectedDate.Value;
            }

            DateTime? untilDate = null;
            if (dtUntilDate.SelectedDate != null)
            {
                untilDate = dtUntilDate.SelectedDate.Value;
            }

            //Obtener reportes segun filtros
            var controller = GatewayController.GetInstance();
            var reports = controller.ListFilteredReports(lowerAge, upperAge, sinceDate, untilDate, isSomker, isDiabetic, isHypertense,
                                           isDyslipidemic);
            
            grReports.DataContext = reports;
        }

        private void grReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonAdd.IsEnabled = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            var controller = GatewayController.GetInstance();
            try
            {
                foreach (Report selectedItem in grReports.SelectedItems)
                {
                    controller.AddReportToInvestigation(selectedItem.UdaId.Value, selectedItem.Patient.UdaId.Value,
                                                        _idInvestigation);
                }

                if (MessageBox.Show("Desea agregar más reportes?", "Confirmacion", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                   grReports.SelectedItems.Clear();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("Error al intentar agregar reporte");
            }


        }
    }
}
