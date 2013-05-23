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
using Entities.Tools;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for OtherInformation.xaml
    /// </summary>
    public partial class OtherInformation : UserControl
    {
        private List<ExampleEffort> _lstEffort;
        private List<ExampleComplication> _lstComplication;
        private List<ExampleMedication> _lstMedication;
        private MedicationSelector _ms;

        public OtherInformation(ToolsReport report)
        {
            InitializeComponent();

            _lstEffort = new List<ExampleEffort>();
            _lstComplication = new List<ExampleComplication>();
            _lstMedication = new List<ExampleMedication>();
        }

        private void btnMedication_Click(object sender, RoutedEventArgs e)
        {
            _ms = new MedicationSelector();
            _ms.ShowDialog();
            if (!String.IsNullOrWhiteSpace(_ms.name))
                txtMedication.Text = _ms.name;
        }

        private void btnAddMedication_Click(object sender, RoutedEventArgs e)
        {
            int hour, min;
            if (int.TryParse(txtHourMedication.Text, out hour)
                && int.TryParse(txtMinMedication.Text, out min)
                && 0 <= hour && hour < 24 && 0 <= min && min < 60
                && !String.IsNullOrWhiteSpace(_ms.category) 
                && !String.IsNullOrWhiteSpace(_ms.active) 
                && !String.IsNullOrWhiteSpace(_ms.name))
            {
                _lstMedication.Add(new ExampleMedication
                    {
                        Hour = hour.ToString("D2") + ":" + min.ToString("D2"),
                        Category = _ms.category,
                        Active = _ms.active,
                        Name = _ms.name
                    });
            }

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
                _lstEffort.Add(new ExampleEffort
                    {
                        Hour = hour.ToString("D2") + ":" + min.ToString("D2"),
                        Type = cmbTypeEffort.Text
                    });
            }
            //TODO show error message when the time is not correct

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

                _lstComplication.Add(new ExampleComplication
                    {
                        Hour = hour.ToString("D2") + ":" + min.ToString("D2"),
                        Type = type
                    });
            }
            //TODO show error message when the time is not correct

            txtHourComp.Clear();
            txtMinComp.Clear();
            cmbTypeComp.SelectedIndex = -1;
            txtCompOther.Clear();
            txtCompOther.IsEnabled = false;
            grComplications.DataContext = _lstComplication;
        }

        private void cmbTypeComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCompOther.IsEnabled = ((ComboBoxItem)e.AddedItems).Content.Equals("Otros");
        }
    }

    public class ExampleEffort
    {
        public string Hour { get; set; }
        public string Type { get; set; }
    }
    public class ExampleComplication
    {
        public string Hour { get; set; }
        public string Type { get; set; }
    }
    public class ExampleMedication
    {
        public string Hour  { get; set; }
        public string Category { get; set; }
        public string Active { get; set; }
        public string Name { get; set; }
    }
}
