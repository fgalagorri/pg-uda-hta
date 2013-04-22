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

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for DiagnosisEditor.xaml
    /// </summary>
    public partial class DiagnosisEditor : Window
    {
        public DiagnosisEditor()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
