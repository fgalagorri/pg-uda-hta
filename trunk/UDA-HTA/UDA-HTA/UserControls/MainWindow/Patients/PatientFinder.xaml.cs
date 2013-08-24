using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientFinder.xaml
    /// </summary>
    public partial class PatientFinder : UserControl
    {
        private UDA_HTA.MainWindow container;

        public PatientFinder(UDA_HTA.MainWindow w)
        {
            InitializeComponent();
            colBirth.Binding.StringFormat = ConfigurationManager.AppSettings["ShortDateString"];
            container = w;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var controller = GatewayController.GetInstance();
            var patients = controller.ListPatients(txtDocument.Text.Trim(), txtName.Text.Trim(), txtSurname.Text.Trim(),
                                                   dtBirthDate.SelectedDate, txtRegistry.Text);

            grPatients.DataContext = patients;
            Mouse.OverrideCursor = null;
        }


        private void grPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid) sender).SelectedIndex != -1)
                container.PatientSelected((PatientSearch) e.AddedItems[0]);
        }


        private void enterSubmit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnSearch_Click(sender, e);
        }
    }
}
