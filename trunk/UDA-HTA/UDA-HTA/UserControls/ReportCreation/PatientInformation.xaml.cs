using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.ReportCreation
{

    /// <summary>
    /// Interaction logic for PatientInformation.xaml
    /// </summary>
    public partial class PatientInformation : UserControl
    {
        private ICollection<EmergencyContact> _emContacts;


        public PatientInformation()
        {
            InitializeComponent();
        }
        public PatientInformation(Report report)
        {
            InitializeComponent();

            if (report.Patient != null)
            {
                var p = report.Patient;

                // Datos importados
                txtNames.Text = p.Names;
                txtSurnames.Text = p.Surnames;
                txtCI.Text = p.DocumentId;
                txtBirthDay.Text = p.BirthDate.HasValue ? p.BirthDate.Value.Day.ToString() : "";
                txtBirthMon.Text = p.BirthDate.HasValue ? p.BirthDate.Value.Month.ToString() : "";
                txtBirthYear.Text = p.BirthDate.HasValue ? p.BirthDate.Value.Year.ToString() : "";
                cmbSex.SelectedIndex = p.Sex.HasValue ? (int)p.Sex.Value : -1;
                txtAddress.Text = p.Address;
                txtNeighbour.Text = p.Neighbour;
                txtCity.Text = p.City;
                cmbDepartment.SelectedValue = p.Department;
                txtTel.Text = p.Phone;
                txtTelAlt.Text = p.Phone2;
                txtCel.Text = p.CellPhone;
                txtMail.Text = p.Email;

                _emContacts = p.EmergencyContactList ?? new List<EmergencyContact>();
            }
        }

        public Report GetReport(Report r)
        {
            int year = int.Parse(txtBirthYear.Text);
            int mon = int.Parse(txtBirthMon.Text);
            int day = int.Parse(txtBirthDay.Text);
            var p = r.Patient ?? new Patient();

            p.Names = txtNames.Text;
            p.Surnames = txtSurnames.Text;
            p.DocumentId = txtCI.Text;
            p.BirthDate = new DateTime(year, mon, day);
            p.Sex = cmbSex.SelectedIndex != -1 ? (SexType?) (cmbSex.SelectedIndex + 1) : null;
            p.Address = txtAddress.Text;
            p.Neighbour = txtNeighbour.Text;
            p.City = txtCity.Text;
            p.Department = cmbDepartment.Text;
            p.Phone = txtTel.Text;
            p.CellPhone = txtCel.Text;
            p.Phone2 = txtTelAlt.Text;
            p.Email = txtMail.Text;

            p.EmergencyContactList = _emContacts;

            r.Patient = p;
            return r;
        }

        public bool IsValid()
        {
            int i;
            return !string.IsNullOrWhiteSpace(txtNames.Text) &&
                   !string.IsNullOrWhiteSpace(txtSurnames.Text) &&
                   !string.IsNullOrWhiteSpace(txtCI.Text) &&
                   int.TryParse(txtBirthDay.Text, out i) &&
                   int.TryParse(txtBirthMon.Text, out i) &&
                   int.TryParse(txtBirthYear.Text, out i) &&
                   cmbSex.SelectedIndex != -1 &&
                   !string.IsNullOrWhiteSpace(txtAddress.Text) &&
                   !string.IsNullOrWhiteSpace(txtNeighbour.Text) &&
                   !string.IsNullOrWhiteSpace(txtCity.Text) &&
                   cmbDepartment.SelectedIndex != -1 &&
                   (!string.IsNullOrWhiteSpace(txtTel.Text) ||
                   !string.IsNullOrWhiteSpace(txtCel.Text)) &&
                   !string.IsNullOrWhiteSpace(txtMail.Text);
        }


        #region Contactos de Emegencia

        private void btnAddEmContact_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtEmCName.Text) &&
                !String.IsNullOrWhiteSpace(txtEmCSurname.Text) &&
                !String.IsNullOrWhiteSpace(txtEmCPhone.Text))
            {
                grContacts.DataContext = null;

                _emContacts.Add(new EmergencyContact
                    {
                        Name = txtEmCName.Text,
                        Surname = txtEmCSurname.Text,
                        Phone = txtEmCPhone.Text
                    });

                txtEmCName.Clear();
                txtEmCSurname.Clear();
                txtEmCPhone.Clear();
                grContacts.DataContext = _emContacts;
            }
        }

        private void grContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemove.IsEnabled = grContacts.SelectedIndex >= 0;
        }

        private void btnRmvEmContact_Click(object sender, RoutedEventArgs e)
        {
            if (grContacts.SelectedIndex >= 0)
            {
                var selItems = grContacts.SelectedItems;
                foreach (EmergencyContact c in selItems)
                    _emContacts.Remove(c);

                grContacts.DataContext = null;
                grContacts.DataContext = _emContacts;
            }
        }

        #endregion
    }
}
