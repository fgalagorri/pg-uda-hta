using System;
using System.Windows;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Administration.UserManagement
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
                controller.ChangePassword(txtOldPassword.Password, txtNewPassword.Password, txtNewPswdRepeat.Password);
                this.Close();
            }
            catch (Exception exception)
            {
                txtNewPassword.Password = "";
                txtOldPassword.Password = "";
                txtNewPswdRepeat.Password = "";
                lblError.Content = exception.Message;
            }
        }
    }
}
