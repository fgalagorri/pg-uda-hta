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
using Gateway;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for PatientConditionEdit.xaml
    /// </summary>
    public partial class BackgroundEdit : UserControl
    {
        private ICollection<MedicalRecord> _lstBackground;
        private Patient _patient;

        private BackgroundEdit()
        {
            InitializeComponent();
            _lstBackground = new List<MedicalRecord>();
        }

        public BackgroundEdit(Patient p)
        {
            InitializeComponent();

            if (p != null)
            {
                _patient = p;   
                _lstBackground = p.Background;
                grBackground.DataContext = null;
                grBackground.DataContext = _lstBackground;
            }
            else
                _lstBackground = new List<MedicalRecord>();
        }

        public Patient GetPatient(Patient p)
        {
            p.Background = _lstBackground;
            return p;
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

    }
}
