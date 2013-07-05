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
                cmbSex.SelectedIndex = p.Sex.HasValue ? (int)p.Sex.Value - 1 : -1;
                txtAddress.Text = p.Address;
                txtNeighbour.Text = p.Neighbour;
                txtCity.Text = p.City;
                cmbDepartment.SelectedValue = p.Department;
                txtTel.Text = p.Phone;
                txtCel.Text = p.CellPhone;
                txtMail.Text = p.Email;

                // Datos posiblemente modificados
                if (report.TemporaryData != null)
                {
                    var t = report.TemporaryData;
                    txtWeight.Text = t.Weight.ToString();
                    txtHeight.Text = t.Height.ToString();
                    _imc = t.BodyMassIndex.HasValue ? t.BodyMassIndex.Value : -1;
                    lblImc.Text = t.BodyMassIndex.ToString();
                    txtFat.Text = t.FatPercentage.ToString();
                    txtMuscle.Text = t.MusclePercentage.ToString();
                    txtKcal.Text = t.Kcal.ToString();
                    chkSmoker.IsChecked = t.Smoker ?? false;
                    chkDiabetic.IsChecked = t.Diabetic ?? false;
                    chkDyslipidemia.IsChecked = t.Dyslipidemia ?? false;
                    chkHypertense.IsChecked = t.Hypertensive ?? false;
                }
                else
                {
                    chkSmoker.IsChecked = false;
                    chkDiabetic.IsChecked = false;
                    chkDyslipidemia.IsChecked = false;
                    chkHypertense.IsChecked = false;
                }
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
            p.Email = txtMail.Text;

            r.Patient = p;
            var t = r.TemporaryData ?? new TemporaryData();

            t.Weight = decimal.Parse(txtWeight.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
            t.Height = decimal.Parse(txtHeight.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
            t.BodyMassIndex = _imc;
            t.FatPercentage = decimal.Parse(txtFat.Text.Replace(",", "."), NumberStyles.Float,
                                            CultureInfo.InvariantCulture);
            t.MusclePercentage = decimal.Parse(txtMuscle.Text.Replace(",", "."), NumberStyles.Float,
                                               CultureInfo.InvariantCulture);
            t.Kcal = int.Parse(txtKcal.Text);
            t.Smoker = chkSmoker.IsChecked.Value;
            t.Diabetic = chkDiabetic.IsChecked.Value;
            t.Dyslipidemia = chkDyslipidemia.IsChecked.Value;
            t.Hypertensive = chkHypertense.IsChecked.Value;

            r.TemporaryData = t;
            return r;
        }

        public bool IsValid()
        {
            int i;
            decimal d;
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
                   !string.IsNullOrWhiteSpace(txtTel.Text) &&
                   !string.IsNullOrWhiteSpace(txtCel.Text) &&
                   !string.IsNullOrWhiteSpace(txtMail.Text) &&
                   decimal.TryParse(txtWeight.Text.Replace(",", "."), NumberStyles.Float,
                                    CultureInfo.InvariantCulture, out d) &&
                   decimal.TryParse(txtHeight.Text.Replace(",", "."), NumberStyles.Float,
                                    CultureInfo.InvariantCulture, out d) &&
                   int.TryParse(txtFat.Text, out i) &&
                   int.TryParse(txtMuscle.Text, out i) &&
                   int.TryParse(txtKcal.Text, out i) &&
                   _imc > 0;
        }

        private void CalculateImc(object sender, TextChangedEventArgs e)
        {
            decimal height, weight;
            string h = txtHeight.Text.Replace(",", ".");

            if (decimal.TryParse(h, NumberStyles.Float, CultureInfo.InvariantCulture, out height)
                && decimal.TryParse(txtWeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out weight))
            {
                _imc = weight/(height*height);
                lblImc.Text = _imc.ToString("0.##", CultureInfo.InvariantCulture);
            }
            else
                lblImc.Text = "";
        }
    }
}
