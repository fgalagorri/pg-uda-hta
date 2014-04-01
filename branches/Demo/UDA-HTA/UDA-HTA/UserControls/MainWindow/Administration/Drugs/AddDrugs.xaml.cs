using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gateway;
using Entities;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow.Administration.Drugs
{
    /// <summary>
    /// Interaction logic for AddDrugs.xaml
    /// </summary>
    public partial class AddDrugs : UserControl
    {
        private Drug _drug;

        public AddDrugs(Drug drug)
        {
            InitializeComponent();

            _drug = drug;
            BindComboBox(comboBoxDrugType);
            if (_drug != null)
            {
                //editar droga
                btnAdd.Content = "Editar";
                btnDelete.Visibility = Visibility.Visible;
                comboBoxDrugType.SelectedValue = drug.Category;
                txtName.Text = drug.Name;
                txtActive.Text = drug.Active;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid())
            {
                MessageBox.Show("Por favor, complete todos los campos", "Datos faltantes",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var controller = GatewayController.GetInstance();
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;

                    if (_drug == null)
                    {
                        //Crear
                        controller.AddDrug(comboBoxDrugType.Text, txtName.Text, txtActive.Text);
                        MessageBox.Show("La droga se ha insertado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        comboBoxDrugType.SelectedIndex = -1;
                        txtName.Text = String.Empty;
                        txtActive.Text = String.Empty;
                    }
                    else
                    {
                        //Editar
                        controller.EditDrug(_drug.Id.Value, comboBoxDrugType.Text, txtName.Text, txtActive.Text);
                        MessageBox.Show("La droga se ha actualizado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    Mouse.OverrideCursor = null;
                }
                catch (Exception exception)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }

        }

        public void BindComboBox(ComboBox comboBoxName)
        {
            var controller = GatewayController.GetInstance();
            var types = controller.GetDrugTypes();
            comboBoxName.ItemsSource = types;
        }

        public bool IsValid()
        {
            return txtName.ValidateString() &
                   txtActive.ValidateString();
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddDrugCategory addc = new AddDrugCategory();
            addc.Show();
            BindComboBox(comboBoxDrugType);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var controller = GatewayController.GetInstance();
                controller.DeleteDrug(_drug.Id.Value);
                MessageBox.Show("La droga se ha eliminado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
        }

    }
}
