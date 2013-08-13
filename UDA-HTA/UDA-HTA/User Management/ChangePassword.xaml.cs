using System;
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
using System.Windows.Shapes;
using Gateway;

namespace UDA_HTA.User_Management
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
