using System;
using System.Windows;
using Gateway;

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
                controller.CreateUser(txtUserName.Text, txtLogin.Text, comboRole.Text, passwordBox.Password);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
