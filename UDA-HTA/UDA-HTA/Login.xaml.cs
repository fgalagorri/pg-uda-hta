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

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            try
            {
                var usr = controller.Login(userLogin.Text.Trim(), userPassword.Password);
                var mainWindow = new MainWindow(usr);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception exception)
            {
                userLogin.Text = " ";
                userPassword.Password = " ";
                lblError.Content = exception.Message;
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void enterSubmit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonLogin_Click(sender, e);
        }
    }
}
