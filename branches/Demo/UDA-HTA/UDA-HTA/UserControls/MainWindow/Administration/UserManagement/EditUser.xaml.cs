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
        private const string EnableTxt = "Habilitar";
        private const string DisableTxt = "Deshabilitar";

        public EditUser(User user)
        {
            InitializeComponent();

            _user = user;
            txtName.Text = _user.Name;
            txtLogin.Text = _user.Login;
            comboRole.Text = _user.Role;

            btnDisable.Content = _user.Enabled ? DisableTxt : EnableTxt;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (IsValid())
                {
                    if (_user.Login != txtLogin.Text && controller.GetUser(txtLogin.Text) != null)
                    {
                        MessageBox.Show("El usuario '" + txtLogin.Text + "' ya existe.", "Error",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        controller.EditUser(_user.Id, txtName.Text, comboRole.Text, txtLogin.Text);
                        Mouse.OverrideCursor = null;
                        MessageBox.Show("Usuario actualizado con éxito", "Éxito");
                    }
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
            return txtName.ValidateString() &
                   comboRole.ValidateSelected() &
                   txtLogin.ValidateString();
        }

        private void btnDisable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user.Enabled)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    var controller = GatewayController.GetInstance();
                    controller.DisableUser(_user.Id);
                    btnDisable.Content = EnableTxt;
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("El usuario se ha deshabilitado correctamente.", "Informacion",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                    _user.Enabled = false;
                }
                else
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    var controller = GatewayController.GetInstance();
                    controller.EnableUser(_user.Id);
                    btnDisable.Content = DisableTxt;
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("El usuario se ha habilitado correctamente.", "Informacion",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                    _user.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
