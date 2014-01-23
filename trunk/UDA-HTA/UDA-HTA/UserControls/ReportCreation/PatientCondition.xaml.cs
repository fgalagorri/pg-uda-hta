using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            try
            {
                _drugs = GatewayController.GetInstance().GetDrugs(null, null, null);
                autoMedication.DataContext = _drugs;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _lstMedication = new List<Medication>();
            _lstBackground = new List<MedicalRecord>();
        }

        public PatientCondition(Report r)
        {
            InitializeComponent();
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            try
            {
                _drugs = GatewayController.GetInstance().GetDrugs(null, null, null);    
                autoMedication.DataContext = _drugs;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


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
                _lstMedication = new List<Medication>();
                _lstBackground = new List<MedicalRecord>();
            }
            CalculateImc(null, null);
            
            _lstBackground = r.Patient.Background ?? new List<MedicalRecord>();
            grBackground.DataContext = _lstBackground;
        }

        public PatientCondition(Patient p)
        {
            InitializeComponent();
            
                colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
                var controller = GatewayController.GetInstance();
                try
                {
                    _drugs = controller.GetDrugs(null, null, null); 
                    autoMedication.DataContext = _drugs;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            if (p != null)
            {
                var tempData = controller.GetPatientLastTempData(p.UdaId.Value);
                if (tempData != null)
                {
                    _lstMedication = tempData.Medication ?? new List<Medication>();
                    grMedication.DataContext = _lstMedication;

                    txtWeight.Text = tempData.Weight.ToString();
                    txtHeight.Text = tempData.Height.HasValue ? tempData.Height.Value.ToString("F") : "";
                    _imc = tempData.BodyMassIndex.HasValue ? tempData.BodyMassIndex.Value : -1;
                    lblImc.Text = tempData.BodyMassIndex.ToString();
                    txtFat.Text = tempData.FatPercentage.ToString();
                    txtMuscle.Text = tempData.MusclePercentage.ToString();
                    txtKcal.Text = tempData.Kcal.ToString();
                    chkSmoker.IsChecked = tempData.Smoker ?? false;
                    chkDiabetic.IsChecked = tempData.Diabetic ?? false;
                    chkDyslipidemia.IsChecked = tempData.Dyslipidemia ?? false;
                    chkHypertense.IsChecked = tempData.Hypertensive ?? false;
                }
                else
                {
                    chkSmoker.IsChecked = false;
                    chkDiabetic.IsChecked = false;
                    chkDyslipidemia.IsChecked = false;
                    chkHypertense.IsChecked = false;
                }
                CalculateImc(null, null);


                _lstBackground = p.Background ?? new List<MedicalRecord>();
                grBackground.DataContext = _lstBackground;

            }
            else
            {
                _lstMedication = new List<Medication>();
                _lstBackground = new List<MedicalRecord>();
            }
        }

        public Report GetReport(Report r)
        {
            var t = r.TemporaryData ?? new TemporaryData();

            foreach (var m in _lstMedication)
                m.Time = m.Time;
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

        public Patient GetPatient(Patient p)
        {
            try
            {
                var t = p.LastTempData ?? new TemporaryData();

                foreach (var m in _lstMedication)
                    m.Time = m.Time;
                t.Medication = _lstMedication;

                decimal number;

                if (!decimal.TryParse(txtWeight.Text.Replace(",", "."),out number))
                {
                    t.Weight = number;                    
                }

                if (!decimal.TryParse(txtHeight.Text.Replace(",", "."), out number))
                {
                    t.Height = number;
                }
                t.BodyMassIndex = _imc;
                
                if (!decimal.TryParse(txtFat.Text.Replace(",", "."), out number))
                {
                    t.FatPercentage = number;
                }
                
                if (!decimal.TryParse(txtMuscle.Text.Replace(",", "."), out number))
                {
                    t.MusclePercentage = number;
                }
                int num;
                if (!int.TryParse(txtKcal.Text, out num))
                {
                    t.Kcal = num;
                }
                t.Smoker = chkSmoker.IsChecked.Value;
                t.Diabetic = chkDiabetic.IsChecked.Value;
                t.Dyslipidemia = chkDyslipidemia.IsChecked.Value;
                t.Hypertensive = chkHypertense.IsChecked.Value;

                p.Background = _lstBackground;
                p.LastTempData = t;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return p;
        }

        public bool IsValid()
        {
            CalculateImc(null, null);

            return txtWeight.ValidateDecimal(0, 400) &
                   txtHeight.ValidateDecimal(0, 3) &
                   txtFat.ValidateDecimal(0, 100) &
                   txtMuscle.ValidateDecimal(0, 100) &
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
            var ms = new MedicationSelector();
            ms.ShowDialog();
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
    }
}
