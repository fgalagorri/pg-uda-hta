using System;
using System.Collections.Generic;
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
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for OtherInformation.xaml
    /// </summary>
    public partial class OtherInformation : UserControl
    {
        private List<Effort> _lstEffort;
        private List<Complication> _lstComplication;
        private List<Medication> _lstMedication;

        public OtherInformation()
        {
            InitializeComponent();
        }
        public OtherInformation(Report report)
        {
            InitializeComponent();

            _lstMedication = report.Carnet.Medications ?? new List<Medication>();
            grMedication.DataContext = _lstMedication;

            _lstEffort = report.Carnet.Efforts ?? new List<Effort>();
            grEffort.DataContext = _lstEffort;

            _lstComplication = report.Carnet.Complications ?? new List<Complication>();
            grComplications.DataContext = _lstComplication;
        }


        public Report GetReport(Report report)
        {
            var carnet = report.Carnet ?? new DailyCarnet();
            foreach (var c in _lstComplication)
                c.Time = DateTimeHelper.SetDateTime(report.BeginDate.Value, c.Time.Hour, c.Time.Minute);
            carnet.Complications = _lstComplication;

            foreach (var e in _lstEffort)
                e.Time = DateTimeHelper.SetDateTime(report.BeginDate.Value, e.Time.Hour, e.Time.Minute);
            carnet.Efforts = _lstEffort;

            foreach (var m in _lstMedication)
                m.Time = DateTimeHelper.SetDateTime(report.BeginDate.Value, m.Time.Hour, m.Time.Minute);
            carnet.Medications = _lstMedication;

            report.Carnet = carnet;
            return report;
        }


        private void btnMedication_Click(object sender, RoutedEventArgs e)
        {
            /*_ms = new MedicationSelector();
            _ms.ShowDialog();
            if (!String.IsNullOrWhiteSpace(_ms.name))
                txtMedication.Text = _ms.name;*/
        }

        private void btnAddMedication_Click(object sender, RoutedEventArgs e)
        {
            grMedication.DataContext = null;

            int hour, min;
            if (int.TryParse(txtHourMedication.Text, out hour)
                && int.TryParse(txtMinMedication.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60)
            {
                // TODO : Ver Drug!!!
                var date = DateTime.MinValue.AddHours(hour).AddMinutes(min);
                _lstMedication.Add(new Medication(date, new Drug("ver", "ver", "ver")));
            }


            // Clears the textboxes after insertion
            txtHourMedication.Clear();
            txtMinMedication.Clear();
            txtMedication.Clear();
            grMedication.DataContext = _lstMedication;
        }

        private void btnAddEffort_Click(object sender, RoutedEventArgs e)
        {
            grEffort.DataContext = null;

            int hour, min;
            if (int.TryParse(txtHourEffort.Text, out hour)
                && int.TryParse(txtMinEffort.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60)
            {
                var date = DateTime.MinValue.AddHours(hour).AddMinutes(min);
                _lstEffort.Add(new Effort(date, cmbTypeEffort.Text));
            }

            //TODO show error message when the time is not correct

            // Clears the textboxes after insertion
            txtHourEffort.Clear();
            txtMinEffort.Clear();
            cmbTypeEffort.SelectedIndex = -1;
            grEffort.DataContext = _lstEffort;
        }

        private void btnAddComp_Click(object sender, RoutedEventArgs e)
        {
            grComplications.DataContext = null;

            int hour, min;
            string type;
            if (int.TryParse(txtHourComp.Text, out hour)
                && int.TryParse(txtMinComp.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60)
            {
                type = cmbTypeComp.Text;
                if (cmbTypeComp.Text.Equals("Otros"))
                    type += ": " + txtCompOther;

                var date = DateTime.MinValue.AddHours(hour).AddMinutes(min);
                _lstComplication.Add(new Complication(date, type));
            }
            //TODO show error message when the time is not correct

            // Clears the textboxes after insertion
            txtHourComp.Clear();
            txtMinComp.Clear();
            cmbTypeComp.SelectedIndex = -1;
            txtCompOther.Clear();
            txtCompOther.IsEnabled = false;
            grComplications.DataContext = _lstComplication;
        }

        private void cmbTypeComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                txtCompOther.IsEnabled = ((ComboBoxItem)e.AddedItems[0]).Content.Equals("Otros");
        }
    }
}
