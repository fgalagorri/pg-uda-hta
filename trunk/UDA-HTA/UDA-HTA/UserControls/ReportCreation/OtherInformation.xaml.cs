using System;
using System.Collections.Generic;
using System.Configuration;
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
        private ICollection<Effort> _lstEffort;
        private ICollection<Complication> _lstComplication;

        public OtherInformation()
        {
            InitializeComponent();
        }
        public OtherInformation(Report report)
        {
            InitializeComponent();
            colEffortTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            colCompTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];

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

            report.Carnet = carnet;
            return report;
        }


        #region Esfuerzo

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
            txtHourEffort.Focus();
        }

        private void btnRmvEffort_Click(object sender, RoutedEventArgs e)
        {
            if (grEffort.SelectedIndex >= 0)
            {
                var selItems = grEffort.SelectedItems;
                foreach (Effort c in selItems)
                    _lstEffort.Remove(c);

                grEffort.DataContext = null;
                grEffort.DataContext = _lstEffort;
            }
        }

        private void grEffort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRmvEffort.IsEnabled = grEffort.SelectedIndex >= 0;
        }

        #endregion


        #region Complicaciones

        private void cmbTypeComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                txtCompOther.IsEnabled = ((ComboBoxItem)e.AddedItems[0]).Content.Equals("Otros");
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
                    type += ": " + txtCompOther.Text;

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
            txtHourComp.Focus();
        }

        private void btnRmvComp_Click(object sender, RoutedEventArgs e)
        {
            if (grComplications.SelectedIndex >= 0)
            {
                var selItems = grComplications.SelectedItems;
                foreach (Complication c in selItems)
                    _lstComplication.Remove(c);

                grComplications.DataContext = null;
                grComplications.DataContext = _lstComplication;
            }
        }

        private void grComplications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRmvComp.IsEnabled = grComplications.SelectedIndex >= 0;
        }

        #endregion
    }
}
