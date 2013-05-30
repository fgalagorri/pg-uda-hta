using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for AdmissionForm.xaml
    /// </summary>
    public partial class AdmissionForm : UserControl
    {
        private ICollection<MedicalRecord> _lstBackground;
        private Report _report;

        public Report Report
        {
            get
            {
                _report.BeginDate = dtStart.SelectedDate.Value.Date +
                                    new TimeSpan(int.Parse(txtStartHour.Text),
                                                 int.Parse(txtStartMinutes.Text), 0);

                var c = _report.Carnet ?? new DailyCarnet();

                c.SleepTimeStart = new DateTime() + // TODO calculate and set the date
                                   new TimeSpan(int.Parse(txtDreamStartHour.Text),
                                                int.Parse(txtDreamStartMinutes.Text), 0);
                c.SleepTimeEnd = new DateTime() +
                                 new TimeSpan(int.Parse(txtDreamEndHour.Text),
                                              int.Parse(txtDreamEndMinutes.Text), 0);
                c.SleepQuality = cmbDreamQty.Text;
                c.SleepQualityDescription = txtDreamDesc.Text;
                c.MealTime = new DateTime() +
                             new TimeSpan(int.Parse(txtMealHour.Text),
                                          int.Parse(txtMealMinutes.Text), 0);

                // Mediciones iniciales
                c.InitSystolic1 = int.Parse(txtSystolicInitial1.Text);
                c.InitSystolic2 = int.Parse(txtSystolicInitial2.Text);
                c.InitSystolic3 = int.Parse(txtSystolicInitial3.Text);
                c.InitDiastolic1 = int.Parse(txtDiastolicInitial1.Text);
                c.InitDiastolic2 = int.Parse(txtDiastolicInitial2.Text);
                c.InitDiastolic3 = int.Parse(txtDiastolicInitial3.Text);
                c.InitHeartRate1 = int.Parse(txtHeartRateInitial1.Text);
                c.InitHeartRate2 = int.Parse(txtHeartRateInitial2.Text);
                c.InitHeartRate3 = int.Parse(txtHeartRateInitial3.Text);

                c.FinalSystolic1 = int.Parse(txtSystolicEnd1.Text);
                c.FinalSystolic2 = int.Parse(txtSystolicEnd2.Text);
                c.FinalSystolic3 = int.Parse(txtSystolicEnd3.Text);
                c.FinalDiastolic1 = int.Parse(txtDiastolicEnd1.Text);
                c.FinalDiastolic2 = int.Parse(txtDiastolicEnd2.Text);
                c.FinalDiastolic3 = int.Parse(txtDiastolicEnd3.Text);
                c.FinalHeartRate1 = int.Parse(txtHeartRateEnd1.Text);
                c.FinalHeartRate2 = int.Parse(txtHeartRateEnd2.Text);
                c.FinalHeartRate3 = int.Parse(txtHeartRateEnd3.Text);

                _report.Carnet = c;
                _report.Patient.Background = _lstBackground;
                
                return _report;
            }
            set
            {
                _report = value;

                dtStart.SelectedDate = _report.BeginDate;
                txtStartHour.Text = _report.BeginDate.HasValue ? _report.BeginDate.Value.Hour.ToString() : "";
                txtStartMinutes.Text = _report.BeginDate.HasValue ? _report.BeginDate.Value.Minute.ToString() : "";

                if (_report.Carnet != null)
                {
                    var c = _report.Carnet;

                    if (c.SleepTimeStart.HasValue)
                    {
                        txtDreamStartHour.Text = c.SleepTimeStart.Value.Hour.ToString();
                        txtDreamStartMinutes.Text = c.SleepTimeStart.Value.Minute.ToString();
                    }
                    if (c.SleepTimeEnd.HasValue)
                    {
                        txtDreamEndHour.Text = c.SleepTimeEnd.Value.Hour.ToString();
                        txtDreamEndMinutes.Text = c.SleepTimeEnd.Value.Minute.ToString();
                    }
                    cmbDreamQty.SelectedValue = c.SleepQuality;
                    txtDreamDesc.Text = c.SleepQualityDescription;
                    if (c.MealTime.HasValue)
                    {
                        txtMealHour.Text = c.MealTime.Value.Hour.ToString();
                        txtMealMinutes.Text = c.MealTime.Value.Minute.ToString();
                    }

                    // mediciones iniciales
                    txtSystolicInitial1.Text = c.InitSystolic1.ToString();
                    txtSystolicInitial2.Text = c.InitSystolic2.ToString();
                    txtSystolicInitial3.Text = c.InitSystolic3.ToString();
                    txtDiastolicInitial1.Text = c.InitDiastolic1.ToString();
                    txtDiastolicInitial2.Text = c.InitDiastolic2.ToString();
                    txtDiastolicInitial3.Text = c.InitDiastolic3.ToString();
                    txtHeartRateInitial1.Text = c.InitHeartRate1.ToString();
                    txtHeartRateInitial2.Text = c.InitHeartRate2.ToString();
                    txtHeartRateInitial3.Text = c.InitHeartRate3.ToString();

                    txtSystolicEnd1.Text = c.FinalSystolic1.ToString();
                    txtSystolicEnd2.Text = c.FinalSystolic2.ToString();
                    txtSystolicEnd3.Text = c.FinalSystolic3.ToString();
                    txtDiastolicEnd1.Text = c.FinalDiastolic1.ToString();
                    txtDiastolicEnd2.Text = c.FinalDiastolic2.ToString();
                    txtDiastolicEnd3.Text = c.FinalDiastolic3.ToString();
                    txtHeartRateEnd1.Text = c.FinalHeartRate1.ToString();
                    txtHeartRateEnd2.Text = c.FinalHeartRate2.ToString();
                    txtHeartRateEnd3.Text = c.FinalHeartRate3.ToString();
                }

                _lstBackground = _report.Patient.Background ?? new List<MedicalRecord>();
                grBackground.DataContext = _lstBackground;
            }
        }

        public AdmissionForm()
        {
            InitializeComponent();
        }

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
        }

        private void btnDelIllness_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
