using System;
using System.Windows;
using System.Windows.Input;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for NewResearch.xaml
    /// </summary>
    public partial class NewResearch : Window
    {
        private UDA_HTA.MainWindow container;
        private Investigation _investigation;

        public NewResearch(UDA_HTA.MainWindow w, Investigation investigation)
        {
            InitializeComponent();
            container = w;
            if (investigation != null)
            {
                //Editar
                _investigation = investigation;
                txtName.Text = investigation.Name;
                txtComment.Text = investigation.Comment;
                dpDate.SelectedDate = investigation.CreationDate;
                btnCreate.Content = "Actualizar";
                btnAddReport.Visibility = Visibility.Hidden;
            }
            else
            {
                //Crear
                btnCreate.Content = "Crear";
                btnAddReport.Visibility = Visibility.Visible;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            try
            {
                if (_investigation != null)
                {
                    //Editar
                    _investigation.Name = txtName.Text;
                    _investigation.CreationDate = dpDate.SelectedDate.Value;
                    _investigation.Comment = txtComment.Text;
                    controller.EditInvestigation(_investigation);
                }
                else
                {
                    //Crear
                    if (dpDate.SelectedDate != null)
                    {
                        _investigation = controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(),
                                                       dpDate.SelectedDate.Value);
                    }
                    else
                    {
                        _investigation = controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(), DateTime.Today);
                    }
                }

                container.ContainerInvestigation.Content = new ResearchViewer(_investigation.IdInvestigation);
                Mouse.OverrideCursor = null;    
                this.Close();
            }
            catch(Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddReports_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            try
            {
                Investigation investigation;
                if (dpDate.SelectedDate != null)
                    investigation = controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(),
                                                   dpDate.SelectedDate.Value);
                else
                    investigation = controller.CreateInvestigation(txtName.Text.Trim(), txtComment.Text.Trim(), DateTime.Today);
                
                Mouse.OverrideCursor = null;
                this.Close();

                var addReportsWindow = new AddReportsToResearch(investigation, container);
                addReportsWindow.Show();

            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }
    }
}
