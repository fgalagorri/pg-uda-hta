using System;
using System.Windows;
using System.Windows.Input;
using Gateway;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow.Administration.UserManagement
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (IsValid())
                {
                    controller.CreateUser(txtUserName.Text, txtLogin.Text, comboRole.Text, passwordBox.Password);
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("El usuario ha sido creado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();                    
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Por favor, complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                    
                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsValid()
        {
            return txtUserName.ValidateString() &
                   comboRole.ValidateSelected() &
                   txtLogin.ValidateString() &
                   passwordBox.Password != null;
        }

    }
}
