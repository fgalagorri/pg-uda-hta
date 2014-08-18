using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Entities;
using Gateway;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.ReportCreation
{

    /// <summary>
    /// Interaction logic for PatientInformation.xaml
    /// </summary>
    public partial class PatientInformation : UserControl
    {
        private ICollection<EmergencyContact> _emContacts;
        private Patient _patient;

        public PatientInformation()
        {
            InitializeComponent();
        }

        public PatientInformation(Patient p)
        {
            _patient = p;
            InitializeComponent();

            if (p != null)
                SetPatient(p);
            else
            {
                _patient = new Patient();
                _emContacts = new List<EmergencyContact>();
            }
        }

        private void SetPatient(Patient p)
        {
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
            txtTel.Text = p.Phone;
            txtTelAlt.Text = p.Phone2;
            txtCel.Text = p.CellPhone;
            txtMail.Text = p.Email;
            txtNroReg.Text = p.RegisterNumber;

            if (!string.IsNullOrWhiteSpace(p.Department))
            {
                var selected = cmbDepartment.Items.Cast<ComboBoxItem>()
                                                  .FirstOrDefault(
                                                      item =>
                                                          String.Equals(item.Content.ToString(), p.Department,
                                                              StringComparison.CurrentCultureIgnoreCase));
                cmbDepartment.SelectedItem = selected;
            }

            grContacts.DataContext = p.EmergencyContactList;

            _emContacts = new List<EmergencyContact>();
            _emContacts = p.EmergencyContactList;
        }

        public Patient GetPatient()
        {
            int year = int.Parse(txtBirthYear.Text);
            int mon = int.Parse(txtBirthMon.Text);
            int day = int.Parse(txtBirthDay.Text);

            _patient.Names = txtNames.Text;
            _patient.Surnames = txtSurnames.Text;
            _patient.DocumentId = Regex.Replace(txtCI.Text, "[^0-9]", "");
            _patient.RegisterNumber = txtNroReg.Text;
            _patient.BirthDate = new DateTime(year, mon, day);
            _patient.Sex = cmbSex.SelectedIndex != -1 ? (SexType?)cmbSex.SelectedIndex : null;
            _patient.Address = txtAddress.Text;
            _patient.Neighbour = txtNeighbour.Text;
            _patient.City = txtCity.Text;
            _patient.Department = cmbDepartment.Text;
            _patient.Phone = txtTel.Text;
            _patient.CellPhone = txtCel.Text;
            _patient.Phone2 = txtTelAlt.Text;
            _patient.Email = txtMail.Text;

            _patient.EmergencyContactList = _emContacts;

            return _patient;
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
            p.RegisterNumber = txtNroReg.Text;
            p.BirthDate = new DateTime(year, mon, day);
            p.Sex = cmbSex.SelectedIndex != -1 ? (SexType?) cmbSex.SelectedIndex : null;
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
            return txtNames.ValidateString() &
                   txtSurnames.ValidateString() &
                   txtCI.ValidateString() &
                   txtBirthDay.ValidateInt(1, 31) &
                   txtBirthMon.ValidateInt(1, 12) &
                   txtBirthYear.ValidateInt(1900, 2100) &
                   cmbSex.ValidateSelected() //&
                   //txtAddress.ValidateString() &
                   //txtNeighbour.ValidateString() &
                   //txtCity.ValidateString() &
                   //cmbDepartment.ValidateSelected() &
                   //(txtTel.ValidateString(false) ||
                   //txtCel.ValidateString(false)) &
                   //txtMail.ValidateString()
                   ;
        }


        #region Contactos de Emegencia

        private void btnAddEmContact_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrWhiteSpace(txtEmCName.Text) ||
                !String.IsNullOrWhiteSpace(txtEmCSurname.Text)) &&
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
                
                grContacts.DataContext = _emContacts.Where(ec => !ec.DeleteContact);
                txtEmCName.Focus();
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
                {
                    if (_emContacts.Contains(c))
                    {
                        _emContacts.Remove(c);
                        c.DeleteContact = true;
                        _emContacts.Add(c);
                    }                    
                }

                grContacts.DataContext = null;
                grContacts.DataContext = _emContacts.Where(ec => !ec.DeleteContact);
            }
        }

        #endregion

        private void btnFindDocumentHC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtCI.Text.Trim()))
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    var p = GatewayController.GetInstance().FindPatientByDocumentHC(txtCI.Text);
                    if (p == null)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show("No se han encontrado coincidencias en la base del HC",
                            "No hay coincidencias", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        var pfHc = new PatientFoundHC(p);
                        Mouse.OverrideCursor = null;
                        bool? usePatient = pfHc.ShowDialog();
                        if (usePatient.HasValue && usePatient.Value)
                        {
                            if (pfHc.UseAllData)
                            {
                                _patient.Merge(p);
                                SetPatient(_patient);
                            }
                            else
                            {
                                _patient.RegisterNumber = p.RegisterNumber;
                                SetPatient(_patient);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El Documento de identidad no ha sido ingresado.",
                            "Verifique el Documento", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnFindRegNoHC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNroReg.Text.Trim()))
                {   
                    Mouse.OverrideCursor = Cursors.Wait;
                    var p = GatewayController.GetInstance().FindPatientByRegNoHC(txtNroReg.Text);
                    if (p == null)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show("No se han encontrado coincidencias en la base del HC",
                            "No hay coincidencias", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        var pfHc = new PatientFoundHC(p);
                        pfHc.HideRegisterButton();
                        Mouse.OverrideCursor = null;
                        bool? usePatient = pfHc.ShowDialog();
                        if (usePatient.HasValue && usePatient.Value)
                        {
                            if (pfHc.UseAllData)
                            {
                                _patient.Merge(p);
                                SetPatient(_patient);
                            }
                            else
                            {
                                _patient.RegisterNumber = p.RegisterNumber;
                                SetPatient(_patient);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El Nro. de Registro no ha sido ingresado.",
                            "Verifique el Nro. de Registro", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
