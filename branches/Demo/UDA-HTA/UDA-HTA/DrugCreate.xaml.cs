using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gateway;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for DrugCreate.xaml
    /// </summary>
    public partial class DrugCreate : Window
    {
        private bool okType = false;
        private bool okActive = false;
        private bool okName = false;


        public DrugCreate()
        {
            InitializeComponent();

            okType = false;
            okActive = false;
            okName = false;

            var controller = GatewayController.GetInstance();
            var types = controller.GetDrugTypes();
            comboBoxDrugType.ItemsSource = types;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                //Crear
                controller.AddDrug(comboBoxDrugType.Text, txtName.Text, txtActive.Text);
                MessageBox.Show("La droga se ha insertado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                comboBoxDrugType.SelectedIndex = -1;
                txtName.Text = String.Empty;
                txtActive.Text = String.Empty;

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

        private void EnableAdd()
        {
            btnAdd.IsEnabled = okType && okActive && okName;
        }
        
        private void ComboBoxDrugType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            okType = comboBoxDrugType.SelectedIndex >= 0;
            EnableAdd();
        }

        private void TxtName_OnKeyDown(object sender, KeyEventArgs e)
        {
            okName = txtName.Text.Length > 0;
            EnableAdd();
        }

        private void TxtActive_OnKeyDown(object sender, KeyEventArgs e)
        {
            okActive = txtActive.Text.Length > 0;
            EnableAdd();
        }
    }
}
