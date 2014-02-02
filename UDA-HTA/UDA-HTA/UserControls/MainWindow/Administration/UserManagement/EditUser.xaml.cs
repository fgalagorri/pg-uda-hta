using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Entities;
using Gateway;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow.Administration.UserManagement
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : UserControl
    {
        private User _user;

        public EditUser(User user)
        {
            InitializeComponent();

            _user = user;
            txtName.Text = _user.Name;
            txtLogin.Text = _user.Login;
            comboRole.Text = _user.Role;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (IsValid())
                {
                    controller.EditUser(_user.Id, txtName.Text, comboRole.Text, txtLogin.Text);
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Usuario actualizado con éxito");                    
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Por favor, complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                    
                }
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsValid()
        {
            return txtName.ValidateString() &
                   comboRole.ValidateSelected() &
                   txtLogin.ValidateString();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var controller = GatewayController.GetInstance();
                controller.DeleteUser(_user.Id);
                MessageBox.Show("El usuario se ha eliminado", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
