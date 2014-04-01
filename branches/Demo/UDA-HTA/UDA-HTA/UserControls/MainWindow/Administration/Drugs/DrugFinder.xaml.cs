using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gateway;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Administration.Drugs
{
    /// <summary>
    /// Interaction logic for DrugFinder.xaml
    /// </summary>
    public partial class DrugFinder : UserControl
    {
        private UDA_HTA.MainWindow container;

        public DrugFinder(UDA_HTA.MainWindow w)
        {
            InitializeComponent();
            container = w;
        }

        private void grDrugs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            container.btnEditDrugs.IsEnabled = true;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            try
            {
                var drugs = controller.GetDrugs(txtType.Text, txtActive.Text, txtName.Text);
                grDrugs.DataContext = drugs;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Mouse.OverrideCursor = null;
        }

        public Drug GetSelectedDrug()
        {
            return (Drug)grDrugs.SelectedItem;
        }

        private void GrDrugs_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            container.btnEditDrugs.IsEnabled = true;
            if (GetSelectedDrug() != null)
            {
                var d = GetSelectedDrug();
                container.ContainerAdministration.Content = new AddDrugs(d);
            }

        }
    }
}
