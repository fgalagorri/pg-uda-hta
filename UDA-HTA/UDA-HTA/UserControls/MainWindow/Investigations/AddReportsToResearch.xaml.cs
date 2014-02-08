using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gateway;
using Entities;
using MessageBox = System.Windows.MessageBox;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for AddReportsToResearch.xaml
    /// </summary>
    public partial class AddReportsToResearch : Window
    {
        private UDA_HTA.MainWindow container;
        private Investigation _investigation;
        private ICollection<Report> _lstReport;
        private ICollection<long?> reportIds; 

        public AddReportsToResearch(Investigation investigation, UDA_HTA.MainWindow w)
        {
            InitializeComponent();
            
            _investigation = investigation;
            reportIds = _investigation.LReports.Select(r => r.UdaId).ToList();

            buttonAdd.IsEnabled = false;
            container = w;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            tbTotal.Visibility = Visibility.Visible;
            int? lowerAge = null;
            int? upperAge = null;

            var error = false;

            if (txtEdad.Text.Length > 0)
            {
                //parsear filtro edad
                string text = txtEdad.Text.Trim();
                char firstChar = text[0];
                char[] destination = new char[text.Length - 1];

                string[] ageBetween;


                try
                {
                    if (cbSign.Text.Equals("Igual a"))
                    {
                        lowerAge = int.Parse(text);
                        upperAge = int.Parse(text);
                    }
                    else
                    {
                        if (cbSign.Text.Equals("Mayor a"))
                        {
                            lowerAge = int.Parse(text);
                        }
                        else
                        {
                            if (cbSign.Text.Equals("Menor a"))
                            {
                                upperAge = int.Parse(text);
                            }
                            else
                            {
                                int val = int.Parse(firstChar.ToString());
                                if (val >= 0 || val <= 9)
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
                                    error = true;
                                }
                            }
                        }
                    }

                }
                catch
                {
                    error = true;
                }
            }

            if (!error)
            {
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
                try
                {
                    _lstReport = controller.ListFilteredReports(lowerAge, upperAge, sinceDate, untilDate, isSomker,
                                                                isDiabetic, isHypertense,
                                                                isDyslipidemic);

                    // Filtro solo las que no se encuentran en la investigación
                    _lstReport = _lstReport.Where(r => !reportIds.Contains(r.UdaId)).ToList();

                    quantTotal.Text = " " + _lstReport.Count.ToString();
                    grReports.DataContext = _lstReport;
                    Mouse.OverrideCursor = null;
                }
                catch (Exception exception)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }else
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("El formato del campo edad es incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void grReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonAdd.IsEnabled = ((DataGrid)sender).SelectedItems.Count > 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                foreach (Report selectedItem in grReports.SelectedItems)
                {
                    controller.AddReportToInvestigation(selectedItem.UdaId.Value, selectedItem.Patient.UdaId.Value,
                                                        _investigation.IdInvestigation);

                    reportIds.Add(selectedItem.UdaId.Value);
                }

                if (MessageBox.Show("Desea agregar más reportes?", "Confirmacion", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {                    
                    //Eliminar selectedItems
                    if (grReports.SelectedIndex >= 0)
                    {
                        var selItems = grReports.SelectedItems;
                        foreach (Report r in selItems)
                            _lstReport.Remove(r);

                        grReports.DataContext = null;
                        grReports.DataContext = _lstReport;
                    }

                }

                else
                {
                    this.Close();
                }
                
                container.ContainerInvestigation.Content = new ResearchViewer(_investigation.IdInvestigation);
                
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
