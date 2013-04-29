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
        public MedicationSelector()
        {
            InitializeComponent();

            cmbCategory.Items.Add("Betabloqueante");
            cmbCategory.Items.Add("Diuréticos");
            cmbCategory.Items.Add("Calcioantagonistas"); 
            cmbCategory.Items.Add("IECA");
            cmbCategory.Items.Add("ARA2");
            cmbCategory.Items.Add("Acción Central");
            
            cmbActive
    }
}
