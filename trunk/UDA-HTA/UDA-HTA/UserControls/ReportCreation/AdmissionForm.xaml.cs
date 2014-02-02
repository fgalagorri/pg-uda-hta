using System.Windows.Controls;
using Entities;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for AdmissionForm.xaml
    /// </summary>
    public partial class AdmissionForm : UserControl
    {
        private const int MAXSYS = 200;
        private const int MAXDIAS = 200; 
        private const int MINSYS = 20;
        private const int MINDIAS = 20;
        private const int MAXHR = 200;
        private const int MINHR = 20;


        public AdmissionForm()
        {
            InitializeComponent();
        }
        public AdmissionForm(Report r)
        {
            InitializeComponent();

            dtStart.SelectedDate = r.BeginDate;
            if (r.BeginDate.HasValue)
            {
                txtStartHour.Text = FormatInt(r.BeginDate.Value.Hour);
                txtStartMinutes.Text = FormatInt(r.BeginDate.Value.Minute);
            }

            txtRequester.Text = r.Requester;

            if (r.Carnet != null)
            {
                var c = r.Carnet;

                if (c.SleepTimeStart.HasValue)
                {
                    txtDreamStartHour.Text = FormatInt(c.SleepTimeStart.Value.Hour);
                    txtDreamStartMinutes.Text = FormatInt(c.SleepTimeStart.Value.Minute);
                }

                if (c.SleepTimeEnd.HasValue)
                {
                    txtDreamEndHour.Text = FormatInt(c.SleepTimeEnd.Value.Hour);
                    txtDreamEndMinutes.Text = FormatInt(c.SleepTimeEnd.Value.Minute);
                }

                cmbDreamQty.SelectedValue = c.SleepQuality;
                txtDreamDesc.Text = c.SleepQualityDescription;
                if (c.MealTime.HasValue)
                {
                    txtMealHour.Text = FormatInt(c.MealTime.Value.Hour);
                    txtMealMinutes.Text = FormatInt(c.MealTime.Value.Minute);
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
        }

        // Dado un numero entero, le da formato para imprimir
        public string FormatInt(int number)
        {
            string result = "";
            
            if (number < 10)
            {
                result += "0";
            }
            result += number.ToString();
            
            return result;
        }


        public Report GetReport(Report report)
        {
            report.BeginDate = dtStart.SelectedDate.Value.Date
                                       .AddHours(int.Parse(txtStartHour.Text))
                                       .AddMinutes(int.Parse(txtStartMinutes.Text));

            report.TemporaryData.Age = report.Patient.BirthDate.Value
                                             .CalculateAge(report.BeginDate);
            report.Requester = txtRequester.Text;

            var c = report.Carnet ?? new DailyCarnet();

            c.SleepTimeStart = DateTimeHelper.SetDateTime(report.BeginDate.Value,
                                                          int.Parse(txtDreamStartHour.Text),
                                                          int.Parse(txtDreamStartMinutes.Text));

            c.SleepTimeEnd = DateTimeHelper.SetDateTime(report.BeginDate.Value,
                                                        int.Parse(txtDreamEndHour.Text),
                                                        int.Parse(txtDreamEndMinutes.Text));
            c.SleepQuality = cmbDreamQty.Text;
            c.SleepQualityDescription = txtDreamDesc.Text;
            c.MealTime = DateTimeHelper.SetDateTime(report.BeginDate.Value,
                                                    int.Parse(txtMealHour.Text),
                                                    int.Parse(txtMealMinutes.Text));

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

            report.Carnet = c;

            return report;
        }


        public bool IsValid()
        {
            return dtStart.ValidateDate() &
                   txtStartHour.ValidateInt(0, 23) &
                   txtStartMinutes.ValidateInt(0, 59) &
                   txtDreamStartHour.ValidateInt(0, 23) &
                   txtDreamStartMinutes.ValidateInt(0, 59) &
                   txtDreamEndHour.ValidateInt(0, 23) &
                   txtDreamEndMinutes.ValidateInt(0, 60) &
                   cmbDreamQty.ValidateSelected() &
                   txtMealHour.ValidateInt(0, 23) &
                   txtMealMinutes.ValidateInt(0, 59) &
                   txtSystolicInitial1.ValidateInt(MINSYS, MAXSYS) &
                   txtSystolicInitial2.ValidateInt(MINSYS, MAXSYS) &
                   txtSystolicInitial3.ValidateInt(MINSYS, MAXSYS) &
                   txtDiastolicInitial1.ValidateInt(MINDIAS, MAXDIAS) &
                   txtDiastolicInitial2.ValidateInt(MINDIAS, MAXDIAS) &
                   txtDiastolicInitial3.ValidateInt(MINDIAS, MAXDIAS) &
                   txtHeartRateInitial1.ValidateInt(MINHR, MAXHR) &
                   txtHeartRateInitial2.ValidateInt(MINHR, MAXHR) &
                   txtHeartRateInitial3.ValidateInt(MINHR, MAXHR) &
                   txtSystolicEnd1.ValidateInt(MINSYS, MAXSYS) &
                   txtSystolicEnd2.ValidateInt(MINSYS, MAXSYS) &
                   txtSystolicEnd3.ValidateInt(MINSYS, MAXSYS) &
                   txtDiastolicEnd1.ValidateInt(MINDIAS, MAXDIAS) &
                   txtDiastolicEnd2.ValidateInt(MINDIAS, MAXDIAS) &
                   txtDiastolicEnd3.ValidateInt(MINDIAS, MAXDIAS) &
                   txtHeartRateEnd1.ValidateInt(MINHR, MAXHR) &
                   txtHeartRateEnd2.ValidateInt(MINHR, MAXHR) &
                   txtHeartRateEnd3.ValidateInt(MINHR, MAXHR);
        }
    }
}
