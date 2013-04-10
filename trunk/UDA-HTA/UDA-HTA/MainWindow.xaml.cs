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
using UDA_HTA.UserControls;
using UDA_HTA.UserControls.MainWindow;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PatientViewer patientViewer;

        public MainWindow()
        {
            InitializeComponent();

            patientViewer = new PatientViewer();
            Container.Content = patientViewer;
        }

        private void MenuRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnNewReport_Click(object sender, RoutedEventArgs e)
        {
            var newReportPopup = new NewReportFinder { Owner = this };
            newReportPopup.ShowDialog();
        }

        private void btnReportComments_Click(object sender, RoutedEventArgs e)
        {
            var reportComments = new ReportComments();
            Container.Content = reportComments;
        }
    }
}
