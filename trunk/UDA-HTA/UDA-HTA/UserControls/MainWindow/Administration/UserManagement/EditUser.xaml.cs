using System.Windows;
using System.Windows.Controls;
using Entities;

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
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
