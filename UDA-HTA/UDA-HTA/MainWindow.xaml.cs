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
using Entities;
using Gateway;
using UDA_HTA.UserControls;
using UDA_HTA.UserControls.MainWindow;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Container.Content = new PatientFinder(this);
        }


        #region Ribbon Buttons Click

        private void btnNewReport_Click(object sender, RoutedEventArgs e)
        {
            var newReportPopup = new NewReportFinder { Owner = this };
            newReportPopup.ShowDialog();
        }

        private void btnReportComments_Click(object sender, RoutedEventArgs e)
        {
            var diagnosis = new DiagnosisEditor {Owner = this};
            diagnosis.ShowDialog();
        }

        private void btnFindPatient_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new PatientFinder(this); ;
        }

        #endregion


        private void MenuRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void PatientSelected(PatientSearch patient)
        {
            if (patient.UdaId.HasValue)
                Container.Content = new PatientViewer(patient);
        }
    }
}
