using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;
using Gateway;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for PatientCondition.xaml
    /// </summary>
    public partial class PatientCondition : UserControl
    {
        decimal _imc;
        private ICollection<Medication> _lstMedication;
        private ICollection<MedicalRecord> _lstBackground;
        private ICollection<Drug> _drugs;

        public PatientCondition()
        {
            InitializeComponent();
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            _drugs = GatewayController.GetInstance().GetDrugs(null, null, null);
            autoMedication.DataContext = _drugs;
        }
        public PatientCondition(Report r)
        {
            InitializeComponent();
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            _drugs = GatewayController.GetInstance().GetDrugs(null, null, null);
            autoMedication.DataContext = _drugs;

            // Datos posiblemente modificados
            if (r.TemporaryData != null)
            {
                _lstMedication = r.TemporaryData.Medication ?? new List<Medication>();
                grMedication.DataContext = _lstMedication;

                var t = r.TemporaryData;
                txtWeight.Text = t.Weight.ToString();
                txtHeight.Text = t.Height.HasValue ? t.Height.Value.ToString("F") : "";
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
            CalculateImc(null, null);
            
            _lstBackground = r.Patient.Background ?? new List<MedicalRecord>();
            grBackground.DataContext = _lstBackground;
        }

        public Report GetReport(Report r)
        {
            var t = r.TemporaryData ?? new TemporaryData();

            foreach (var m in _lstMedication)
                m.Time = DateTimeHelper.SetDateTime(r.BeginDate.Value, m.Time.Hour, m.Time.Minute);
            t.Medication = _lstMedication;

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
            r.Patient.Background = _lstBackground;
            return r;
        }

        public bool IsValid()
        {
            CalculateImc(null, null);

            return txtWeight.ValidateDecimal(0, 400) &
                   txtHeight.ValidateDecimal(0, 3) &
                   txtFat.ValidateInt(0, 100) &
                   txtMuscle.ValidateInt(0, 100) &
                   txtKcal.ValidateInt(0, int.MaxValue) &
                   _imc > 0;
        }


        private void CalculateImc(object sender, TextChangedEventArgs e)
        {
            decimal height, weight;
            string h = txtHeight.Text.Replace(",", ".");
            string w = txtWeight.Text.Replace(",", ".");

            if (decimal.TryParse(h, NumberStyles.Float, CultureInfo.InvariantCulture, out height)
                && decimal.TryParse(w, NumberStyles.Float, CultureInfo.InvariantCulture, out weight))
            {
                _imc = weight / (height * height);
                lblImc.Text = _imc.ToString("0.##", CultureInfo.InvariantCulture);
            }
            else
                lblImc.Text = "";
        }


        #region Antecedentes Médicos

        private void btnAddIllness_Click(object sender, RoutedEventArgs e)
        {
            grBackground.DataContext = null;

            _lstBackground.Add(new MedicalRecord
            {
                Illness = txtIllness.Text,
                Comment = txtComments.Text
            });

            txtIllness.Clear();
            txtComments.Clear();
            grBackground.DataContext = _lstBackground;
            txtIllness.Focus();
        }

        private void btnDelIllness_Click(object sender, RoutedEventArgs e)
        {
            if (grBackground.SelectedIndex >= 0)
            {
                var selItems = grBackground.SelectedItems;
                foreach (MedicalRecord c in selItems)
                    _lstBackground.Remove(c);

                grBackground.DataContext = null;
                grBackground.DataContext = _lstBackground;
            }
        }

        private void grBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelIllness.IsEnabled = grBackground.SelectedIndex >= 0;
        }

        #endregion


        #region Medicación

        private void btnMedication_Click(object sender, RoutedEventArgs e)
        {
            /*_ms = new MedicationSelector();
            _ms.ShowDialog();
            if (!String.IsNullOrWhiteSpace(_ms.name))
                txtMedication.Text = _ms.name;*/
        }

        private void btnAddMedication_Click(object sender, RoutedEventArgs e)
        {
            int hour, min;
            var drug = _drugs.FirstOrDefault(d => d.Name == autoMedication.Text);

            if (int.TryParse(txtHourMedication.Text, out hour)
                && int.TryParse(txtMinMedication.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60
                && drug != null)
            {
                var date = DateTime.MinValue.AddHours(hour).AddMinutes(min);
                _lstMedication.Add(new Medication(date, drug));

                // Clears the textboxes after insertion
                txtHourMedication.Clear();
                txtMinMedication.Clear();
                autoMedication.Text = String.Empty;
                txtHourMedication.Focus();
            }
            else if (!(int.TryParse(txtHourMedication.Text, out hour)
                && int.TryParse(txtMinMedication.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60))
            {
                MessageBox.Show("La hora no está en formato correcto.", "Error!", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("La droga indicada no coincide con una en el sistema.", "Error!", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }

            grMedication.DataContext = _lstMedication;
        }

        private void btnRmvMedication_Click(object sender, RoutedEventArgs e)
        {
            if (grMedication.SelectedIndex >= 0)
            {
                var selItems = grMedication.SelectedItems;
                foreach (Medication c in selItems)
                    _lstMedication.Remove(c);

                grMedication.DataContext = null;
                grMedication.DataContext = _lstMedication;
            }
        }

        private void grMedication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRmvMedication.IsEnabled = grMedication.SelectedIndex >= 0;
        }

        #endregion
    }
}
