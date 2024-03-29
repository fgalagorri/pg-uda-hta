﻿using System;
using System.Windows;
using System.Windows.Input;
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
            userLogin.Focus();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            try
            {
                bool enabled;
                var usr = controller.Login(userLogin.Text.Trim(), userPassword.Password, out enabled);
                
                if (!enabled)
                {
                    userPassword.Password = "";
                    lblError.Content = "El usuario ha sido deshabilitado.";
                }
                else if (usr != null)
                {
                    var mainWindow = new MainWindow(usr);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    userPassword.Password = "";
                    lblError.Content = "Nombre de usuario y/o contraseña incorrectos.";
                }
            }
            catch (Exception exception)
            {
                userPassword.Password = "";
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
