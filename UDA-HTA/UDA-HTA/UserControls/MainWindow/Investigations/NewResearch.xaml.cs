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

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for NewResearch.xaml
    /// </summary>
    public partial class NewResearch : Window
    {
        public NewResearch()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                if (dpDate.SelectedDate != null)
                    controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(),
                                                   dpDate.SelectedDate.Value);
                else
                    controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(), DateTime.Today);
            }
            catch(Exception)
            {
                MessageBoxResult result = MessageBox.Show("Error al intenter crear la investigacion", "Error");
            }
            this.Close();
        }

        private void btnAddReports_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            try
            {
                int idInvestigation = 0;
                if (dpDate.SelectedDate != null)
                    idInvestigation = controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(),
                                                   dpDate.SelectedDate.Value);
                else
                    idInvestigation = controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(), DateTime.Today);
                this.Close();

                var addReportsWindow = new AddReportsToResearch(idInvestigation);
                addReportsWindow.Show();

            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Error al intentar crear la investigacion", "Error");
                this.Close();
            }

        }
    }
}
