using System;
using System.Windows;
using System.Windows.Input;
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
                Mouse.OverrideCursor = Cursors.Wait;
                if (txtNewPassword.Password == txtNewPswdRepeat.Password)
                {
                    controller.ChangePassword(txtOldPassword.Password, txtNewPassword.Password,
                                              txtNewPswdRepeat.Password);
                    MessageBox.Show("Su contraseña ha sido cambiada con éxito", "Éxito");
                    Close();
                }
                else
                {
                    txtNewPassword.Password = "";
                    txtOldPassword.Password = "";
                    txtNewPswdRepeat.Password = "";
                    lblError.Content = "Las contraseñas no coinciden.";
                }
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                txtNewPassword.Password = "";
                txtOldPassword.Password = "";
                txtNewPswdRepeat.Password = "";
                lblError.Content = exception.Message;
            }

            Mouse.OverrideCursor = null;
        }
    }
}
