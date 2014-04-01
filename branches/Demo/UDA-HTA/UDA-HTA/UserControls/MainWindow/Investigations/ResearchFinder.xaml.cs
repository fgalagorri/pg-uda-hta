using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            int? id = null;
            if (txtId.Text.Length > 0)
            {
                id = int.Parse(txtId.Text);
            }
            try
            {
                var researches = controller.ListInvestigations(id, txtName.Text, dtCreationDate.SelectedDate);
                grResearch.DataContext = researches;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void researchId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnSearch_Click(sender, e);
        }

        private void grResearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid) sender).SelectedIndex != -1)
            {
                var i = (InvestigationSearch) e.AddedItems[0];
                container.InvestigationSelected(i.IdInvestigation);
            }
        }
    }
}
