using System;
using System.Globalization;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.ReportCreation
{

    /// <summary>
    /// Interaction logic for PatientInformation.xaml
    /// </summary>
    public partial class PatientInformation : UserControl
    {
        decimal _imc;
        private Report _report;

        public Report Report
        {
            get
            {
                int year = int.Parse(txtBirthYear.Text);
                int mon = int.Parse(txtBirthMon.Text);
                int day = int.Parse(txtBirthDay.Text);
                var p = _report.Patient ?? new Patient();

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
                p.Email = txtMail.Text;

                _report.Patient = p;
                var t = _report.TemporaryData ?? new TemporaryData();

                t.Weight = decimal.Parse(txtWeight.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
                t.Height = decimal.Parse(txtHeight.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
                t.BodyMassIndex = _imc;
                t.FatPercentage = decimal.Parse(txtFat.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
                t.MusclePercentage = decimal.Parse(txtMuscle.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
                t.Kcal = int.Parse(txtKcal.Text);
                t.Smoker = chkSmoker.IsChecked.Value;
                t.Diabetic = chkDiabetic.IsChecked.Value;
                t.Dyslipidemia = chkDyslipidemia.IsChecked.Value;
                t.Hypertensive = chkHypertense.IsChecked.Value;

                _report.TemporaryData = t;
                return _report;
            }
            set { 
                _report = value;
                
                if (_report.Patient != null)
                {
                    var p = _report.Patient;

                    // Datos importados
                    txtNames.Text = p.Names;
                    txtSurnames.Text = p.Surnames;
                    txtCI.Text = p.DocumentId;
                    txtBirthDay.Text = p.BirthDate.HasValue ? p.BirthDate.Value.Day.ToString() : "";
                    txtBirthMon.Text = p.BirthDate.HasValue ? p.BirthDate.Value.Month.ToString() : "";
                    txtBirthYear.Text = p.BirthDate.HasValue ? p.BirthDate.Value.Year.ToString() : "";
                    cmbSex.SelectedIndex = p.Sex.HasValue ? (int) p.Sex.Value - 1 : -1;
                    txtAddress.Text = p.Address;
                    txtNeighbour.Text = p.Neighbour;
                    txtCity.Text = p.City;
                    cmbDepartment.SelectedValue = p.Department;
                    txtTel.Text = p.Phone;
                    txtCel.Text = p.CellPhone;
                    txtMail.Text = p.Email;

                    // Datos posiblemente modificados
                    if (_report.TemporaryData != null)
                    {
                        var t = _report.TemporaryData;
                        txtWeight.Text = t.Weight.ToString();
                        txtHeight.Text = t.Height.ToString();
                        _imc = t.BodyMassIndex.HasValue ? t.BodyMassIndex.Value : -1;
                        lblImc.Text = t.BodyMassIndex.ToString();
                        txtFat.Text = t.FatPercentage.ToString();
                        txtMuscle.Text = t.MusclePercentage.ToString();
                        txtKcal.Text = t.Kcal.ToString();
                        chkSmoker.IsChecked = t.Smoker;
                        chkDiabetic.IsChecked = t.Diabetic;
                        chkDyslipidemia.IsChecked = t.Dyslipidemia;
                        chkHypertense.IsChecked = t.Hypertensive;
                    }
                }
            }
        }

        public PatientInformation()
        {
            InitializeComponent();
        }

        private void CalculateImc(object sender, TextChangedEventArgs e)
        {
            decimal height, weight;
            txtHeight.Text.Replace(",", ".");

            if (decimal.TryParse(txtHeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out height)
                && decimal.TryParse(txtWeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out weight))
            {
                _imc = (decimal) (weight/(height*height));
                lblImc.Text = _imc.ToString("0.##", CultureInfo.InvariantCulture);
            }
            else
                lblImc.Text = "";
        }
    }
}
