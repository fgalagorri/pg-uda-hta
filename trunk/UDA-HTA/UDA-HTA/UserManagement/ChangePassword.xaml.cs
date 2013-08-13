using System;
using System.Windows;
using Gateway;

namespace UDA_HTA.UserManagement
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                controller.ChangePassword(txtLogin.Text, txtOldPassword.Password, txtNewPassword.Password);
                this.Close();
            }
            catch (Exception exception)
            {
                txtLogin.Text = "";
                txtNewPassword.Password = "";
                txtOldPassword.Password = "";
                lblError.Content = exception.Message;
            }
        }
    }
}
