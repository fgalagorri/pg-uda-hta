using System;
using Gateway;
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

namespace UDA_HTA.UserControls.MainWindow.Administration.Drugs
{
    /// <summary>
    /// Interaction logic for AddDrugCategory.xaml
    /// </summary>
    public partial class AddDrugCategory : Window
    {
        public AddDrugCategory()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                //Crear
                controller.CreateDrugType(txtName.Text);
                MessageBox.Show("La categoría se ha insertado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                txtName.Text = String.Empty;

                DialogResult = true;
                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }

        }

        private void TxtName_OnKeyDown(object sender, KeyEventArgs e)
        {
            btnAdd.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
