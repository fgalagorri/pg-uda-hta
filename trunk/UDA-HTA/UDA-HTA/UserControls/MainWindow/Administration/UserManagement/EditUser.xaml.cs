using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Entities;
using Gateway;

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
                controller.EditUser(_user.Id, txtName.Text, comboRole.Text, txtLogin.Text);
                MessageBox.Show("Usuario actualizado con éxito");
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
