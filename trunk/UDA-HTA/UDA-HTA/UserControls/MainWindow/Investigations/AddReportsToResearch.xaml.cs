﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
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

        public AddReportsToResearch(Investigation investigation, UDA_HTA.MainWindow w)
        {
            _investigation = investigation;

            InitializeComponent();

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
                    quantTotal.Text = " " + _lstReport.Count.ToString();
                    grReports.DataContext = _lstReport;
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
            buttonAdd.IsEnabled = true;
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