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
using System.Windows.Shapes;

namespace UDA_HTA.UserControls
{
    /// <summary>
    /// Interaction logic for MedicationSelector.xaml
    /// </summary>
    public partial class MedicationSelector : Window
    {
        public string category { get; set; }
        public string active { get; set; }
        public string name { get; set; }

        public MedicationSelector()
        {
            InitializeComponent();

            cmbCategory.Items.Add("Acción Central");
            cmbCategory.Items.Add("ARA2");
            cmbCategory.Items.Add("Betabloqueante");
            cmbCategory.Items.Add("Calcioantagonistas");
            cmbCategory.Items.Add("Diuréticos");
            cmbCategory.Items.Add("IECA");
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedIndex >= 0)
            {
                cmbActive.Items.Clear();
                cmbActive.Items.Add(cmbCategory.SelectedValue + " item 1");
                cmbActive.Items.Add(cmbCategory.SelectedValue + " item 2");
                cmbActive.Items.Add(cmbCategory.SelectedValue + " item 3");
                cmbActive.Items.Add(cmbCategory.SelectedValue + " item 4");
                cmbActive.SelectedIndex = -1;
                cmbActive.IsEnabled = true;
            }
            else
            {
                cmbActive.SelectedIndex = -1;
                cmbActive.Items.Clear();
                cmbActive.IsEnabled = false;
            }

            cmbName.IsEnabled = false;
            cmbName.Items.Clear();
            cmbName.SelectedIndex = -1;
        }

        private void cmbActive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbActive.SelectedIndex >= 0)
            {
                cmbName.Items.Clear();
                cmbName.Items.Add(cmbActive.SelectedValue + " item 1");
                cmbName.Items.Add(cmbActive.SelectedValue + " item 2");
                cmbName.Items.Add(cmbActive.SelectedValue + " item 3");
                cmbName.Items.Add(cmbActive.SelectedValue + " item 4");
                cmbName.SelectedIndex = -1;
                cmbName.IsEnabled = true;
            }
            else
            {
                cmbName.SelectedIndex = -1;
                cmbName.Items.Clear();
                cmbName.IsEnabled = false;
            }
        }

        private void cmbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbName.SelectedIndex >= 0)
                btnAccept.IsEnabled = true;
            else
                btnAccept.IsEnabled = false;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            category = cmbCategory.SelectedValue.ToString();
            active = cmbActive.SelectedValue.ToString();
            name = cmbName.SelectedValue.ToString();

            Close();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
