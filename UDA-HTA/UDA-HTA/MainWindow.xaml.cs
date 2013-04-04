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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void MenuRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewReport(object sender, RoutedEventArgs e)
        {
            NewReportFinder newReportPopup = new NewReportFinder{Owner = this};
            newReportPopup.ShowDialog();
        }
    }
}
