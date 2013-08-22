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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gateway;

namespace UDA_HTA.UserManagement
{
    /// <summary>
    /// Interaction logic for UserFinder.xaml
    /// </summary>
    public partial class UserFinder : UserControl
    {
        public UserFinder()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            var users = controller.ListUsers(txtName.Text,comboBoxRole.Text,txtLogin.Text);

            grUsers.DataContext = users;
            Mouse.OverrideCursor = null;

        }

        private void grUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
