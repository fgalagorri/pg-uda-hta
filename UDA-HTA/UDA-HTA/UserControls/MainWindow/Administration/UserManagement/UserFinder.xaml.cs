using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gateway;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Administration.UserManagement
{
    /// <summary>
    /// Interaction logic for UserFinder.xaml
    /// </summary>
    public partial class UserFinder : UserControl
    {
        private User _user;
        private UDA_HTA.MainWindow container;

        public UserFinder(UDA_HTA.MainWindow w)
        {
            InitializeComponent();
            container = w;
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
            container.btnEditDoctor.IsEnabled = true;
        }

        public User GetSelectedUser()
        {
            return (User)grUsers.SelectedItem;
        }

    }
}
