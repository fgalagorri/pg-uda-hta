using System.Windows;
using System.Windows.Controls;
using Entities;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerInformation.xaml
    /// </summary>
    public partial class PatientViewerInformation : UserControl
    {
        public PatientViewerInformation()
        {
            InitializeComponent();
        }


        public void SetPatientInfo(Patient patient)
        {
            // Identificación
            lblNames.Text = patient.Names;
            lblLast.Text = patient.Surnames;
            lblCI.Text = patient.DocumentId;
            lblNroReg.Text = patient.RegisterNumber;
            lblBirth.Text = patient.BirthDate.Value.ToShortDateString();
            lblAge.Text = patient.BirthDate.Value.CalculateAge().ToString();
            lblSex.Text = patient.Sex.Value == SexType.M ? "Masculino" : "Femenino";

            // Dirección
            lblAddress.Text = patient.Address;
            lblNeighbour.Text = patient.Neighbour;
            lblCity.Text = patient.City;
            lblDepartament.Text = patient.Department;
            lblTel.Text = patient.Phone;
            lblCel.Text = patient.CellPhone;
            lblTelAlt.Text = patient.Phone2;
            lblMail.Text = patient.Email;

            grContacts.DataContext = null;
            grContacts.DataContext = patient.EmergencyContactList;
        }

        public void CollapseEmergencyContacts()
        {
            EmContact.Visibility = Visibility.Collapsed;
        }
    }
}
