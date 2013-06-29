using System.Windows;
using System.Windows.Controls;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls
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
            container = w;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            long registry;
            var controller = GatewayController.GetInstance();
            bool hasRegistry = long.TryParse(txtRegistry.Text, out registry);
            var patients = controller.ListPatients(txtDocument.Text.Trim(), txtName.Text.Trim(), txtSurname.Text.Trim(),
                                                   dtBirthDate.SelectedDate, hasRegistry ? (long?) registry : null);

            grPatients.DataContext = patients;
        }


        private void grPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid) sender).SelectedIndex != -1)
                container.PatientSelected((PatientSearch) e.AddedItems[0]);
        }
    }
}
