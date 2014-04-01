using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Entities;
using EventLogger;
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
        private bool _isEdit;

        public PatientCondition()
        {
            InitializeComponent();
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            GetDrugs();
            autoMedication.DataContext = _drugs;

            _lstMedication = new List<Medication>();
            _lstBackground = new List<MedicalRecord>();
        }

        public PatientCondition(Report r, bool isEdit)
        {
            InitializeComponent();
            _isEdit = isEdit;
            if (_isEdit)
                GroupBackground.Visibility = Visibility.Collapsed;

            _lstMedication = new List<Medication>();
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            GetDrugs();
            autoMedication.DataContext = _drugs;

            if (r.TemporaryData != null)
            {
                _lstMedication = r.TemporaryData.Medication ?? new List<Medication>();

                var rt = r.TemporaryData;
                txtWeight.Text = rt.Weight.ToString();
                txtHeight.Text = rt.Height.HasValue ? rt.Height.Value.ToString("F") : "";
                _imc = rt.BodyMassIndex.HasValue ? rt.BodyMassIndex.Value : -1;
                lblImc.Text = rt.BodyMassIndex.ToString();
                txtFat.Text = rt.FatPercentage.ToString();
                txtMuscle.Text = rt.MusclePercentage.ToString();
                txtKcal.Text = rt.Kcal.ToString();
                chkSmoker.IsChecked = rt.Smoker ?? false;
                chkDiabetic.IsChecked = rt.Diabetic ?? false;
                chkDyslipidemia.IsChecked = rt.Dyslipidemia ?? false;
                chkHypertense.IsChecked = rt.Hypertensive ?? false;

            }
            else
            {
                chkSmoker.IsChecked = false;
                chkDiabetic.IsChecked = false;
                chkDyslipidemia.IsChecked = false;
                chkHypertense.IsChecked = false;
                _lstMedication = new List<Medication>();
            }

            CalculateImc(null, null);

            if (!_isEdit)
            {
                _lstBackground = r.Patient.Background ?? new List<MedicalRecord>();
                grBackground.DataContext = _lstBackground;
            }
            grMedication.DataContext = _lstMedication;
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
            var ms = new MedicationSelector();
            ms.ShowDialog();

            if (ms.hasNewDrug)
            {
                _drugs = GatewayController.GetInstance().GetDrugs(null, null, null);
                autoMedication.DataContext = _drugs;
            }

            if (!String.IsNullOrWhiteSpace(ms.name))
                autoMedication.Text = ms.name;
        }

        private void btnAddMedication_Click(object sender, RoutedEventArgs e)
        {
            int hour, min;
            var drug = _drugs.FirstOrDefault(d => d.Name == autoMedication.Text);

            if (int.TryParse(txtHourMedication.Text, out hour)
                && int.TryParse(txtMinMedication.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60
                && drug != null
                && txtDose != null)
            {
                grMedication.DataContext = null;
                var date = DateTime.MinValue.AddHours(hour).AddMinutes(min);
                Medication m = new Medication(date, drug);
                m.Dose = txtDose.Text;
                _lstMedication.Add(m);

                // Clears the textboxes after insertion
                txtHourMedication.Clear();
                txtMinMedication.Clear();
                txtDose.Clear();
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


        private void GetDrugs()
        {
            try
            {
                _drugs = GatewayController.GetInstance().GetDrugs(null, null, null);
                autoMedication.DataContext = _drugs;
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"],
                    exception.Message, exception.InnerException);
                MessageBox.Show("Ha ocurrido un error al intentar obtener las drogas.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculateImc(object sender, TextChangedEventArgs e)
        {
            decimal height, weight;
            string h = txtHeight.Text.Replace(",", ".");
            string w = txtWeight.Text.Replace(",", ".");

            if (decimal.TryParse(h, NumberStyles.Float, CultureInfo.InvariantCulture, out height)
                && decimal.TryParse(w, NumberStyles.Float, CultureInfo.InvariantCulture, out weight))
            {
                _imc = Math.Round(weight / (height * height), 2);
                lblImc.Text = _imc.ToString("0.##", CultureInfo.InvariantCulture);
            }
            else
                lblImc.Text = "";
        }


        public Report GetReport(Report r)
        {
            var t = r.TemporaryData ?? new TemporaryData();
            t.Medication = _lstMedication.ToList();

            t.Weight = decimal.Parse(txtWeight.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
            t.Height = decimal.Parse(txtHeight.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture);
            t.BodyMassIndex = _imc;
            if (txtFat.Text != "")
            {
                t.FatPercentage = decimal.Parse(txtFat.Text.Replace(",", "."), NumberStyles.Float,
                                                CultureInfo.InvariantCulture);                
            }
            if (txtMuscle.Text != "")
            {
                t.MusclePercentage = decimal.Parse(txtMuscle.Text.Replace(",", "."), NumberStyles.Float,
                                                   CultureInfo.InvariantCulture);                
            }
            if (txtKcal.Text != "")
            {
                t.Kcal = int.Parse(txtKcal.Text);                
            }

            t.Smoker = chkSmoker.IsChecked.Value;
            t.Diabetic = chkDiabetic.IsChecked.Value;
            t.Dyslipidemia = chkDyslipidemia.IsChecked.Value;
            t.Hypertensive = chkHypertense.IsChecked.Value;

            r.TemporaryData = t;

            if (!_isEdit)
                r.Patient.Background = _lstBackground;
            return r;
        }

        public bool IsValid()
        {
            CalculateImc(null, null);

            return txtWeight.ValidateDecimal(0, 400) &
                   txtHeight.ValidateDecimal(0, 3) &
                   /*
                   txtFat.ValidateDecimal(0, 100) &
                   txtMuscle.ValidateDecimal(0, 100) &
                   txtKcal.ValidateInt(0, int.MaxValue) &
                    */
                   _imc > 0;
                    
        }
    }
}
