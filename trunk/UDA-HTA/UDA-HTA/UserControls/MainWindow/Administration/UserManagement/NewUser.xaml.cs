using System.Windows;
using Gateway;

namespace UDA_HTA.UserManagement
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
            controller.CreateUser(txtUserName.Text,txtLogin.Text,comboRole.Text,passwordBox.Password);
            this.Close();
        }
    }
}
