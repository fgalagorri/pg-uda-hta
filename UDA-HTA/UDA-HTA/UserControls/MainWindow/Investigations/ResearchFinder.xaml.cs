using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for ResearchFinder.xaml
    /// </summary>
    public partial class ResearchFinder : UserControl
    {
        private UDA_HTA.MainWindow container;

        public ResearchFinder(UDA_HTA.MainWindow w)
        {
            InitializeComponent();
            colCreation.Binding.StringFormat = ConfigurationManager.AppSettings["ShortDateString"];
            container = w;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            int? id = int.Parse(txtId.Text.Trim());
            var researches = controller.ListInvestigations(id, txtName.Text.Trim(), dtCreationDate.SelectedDate);

            grResearch.DataContext = researches;
            Mouse.OverrideCursor = null;
        }

        private void researchId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnSearch_Click(sender, e);
        }

        private void grResearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
