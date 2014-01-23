using System;
using System.Collections;
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
                comboBoxDrugType.SelectedValue = drug.Category;
                txtName.Text = drug.Name;
                txtActive.Text = drug.Active;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
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
            catch(Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BindComboBox(ComboBox comboBoxName)
        {
            var controller = GatewayController.GetInstance();
            var types = controller.GetDrugTypes();
            comboBoxName.ItemsSource = types;
        }
    }
}
