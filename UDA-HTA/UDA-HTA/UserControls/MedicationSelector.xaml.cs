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
using Entities;
using Gateway;

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
        public bool hasNewDrug { get; set; }

        private GatewayController _controller;
        private ICollection<string> _types; 
        private ICollection<Drug> _drugs;
        private bool _nameSelected = false;

        public MedicationSelector()
        {
            InitializeComponent();

            _controller = GatewayController.GetInstance();
            _types = _controller.GetDrugTypes();
            _drugs = _controller.GetDrugs(null, null, null);

            cmbCategory.ItemsSource = _types;
            hasNewDrug = false;
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedIndex >= 0)
            {
                var filtered = _drugs.Where(d => d.Category == (string)cmbCategory.SelectedItem).ToList();
                cmbActive.ItemsSource = filtered.GroupBy(d => d.Active).Select(a => a.First().Active).ToList();
                cmbName.ItemsSource = filtered.Select(d => d.Name).ToList();

                cmbActive.SelectedIndex = -1;
                cmbName.SelectedIndex = -1;
                cmbActive.IsEnabled = true;
                cmbName.IsEnabled = true;
            }
            else
            {
                cmbActive.SelectedIndex = -1;
                cmbName.SelectedIndex = -1;
                cmbActive.IsEnabled = false;
                cmbName.IsEnabled = false;
            }
        }

        private void cmbActive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_nameSelected)
            {
                if (cmbActive.SelectedIndex >= 0)
                {
                    cmbName.ItemsSource = _drugs.Where(d => d.Active == (string)cmbActive.SelectedItem
                                                    && d.Category == (string)cmbCategory.SelectedItem)
                        .Select(d => d.Name).ToList();
                    cmbName.SelectedIndex = -1;
                    cmbName.IsEnabled = true;
                }
                else
                {
                    cmbName.SelectedIndex = -1;
                    cmbName.IsEnabled = false;
                }
            }
            _nameSelected = false;
        }

        private void cmbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbName.SelectedIndex >= 0)
            {
                _nameSelected = true;
                cmbActive.SelectedValue = _drugs.First(d => d.Name == (string) cmbName.SelectedItem 
                                                    && d.Category == (string) cmbCategory.SelectedItem).Active;
                btnAccept.IsEnabled = true;
            }
            else
            {
                btnAccept.IsEnabled = false;
            }
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
            var dc = new DrugCreate();
            var returnValue = dc.ShowDialog();

            if (returnValue.HasValue && returnValue.Value)
            {
                _drugs = _controller.GetDrugs(null, null, null);
                cmbCategory.SelectedIndex = -1;
                cmbActive.SelectedIndex = -1;
                cmbName.SelectedIndex = -1;
                hasNewDrug = true;
            }
        }
    }
}
