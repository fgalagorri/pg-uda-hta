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
using Gateway;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Administration
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
            var drugs = controller.GetDrugs(txtType.Text,txtActive.Text,txtName.Text);

            grDrugs.DataContext = drugs;
            Mouse.OverrideCursor = null;
        }

        public Drug GetSelectedDrug()
        {
            return (Drug)grDrugs.SelectedItem;
        }

    }
}
