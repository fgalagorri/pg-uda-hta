using System;
using System.Globalization;
using System.Windows.Controls;

namespace UDA_HTA.UserControls.ReportCreation
{

    /// <summary>
    /// Interaction logic for PatientInformation.xaml
    /// </summary>
    public partial class PatientInformation : UserControl
    {
        double _imc;

        public PatientInformation()
        {
            InitializeComponent();
        }

        private void CalculateImc(object sender, TextChangedEventArgs e)
        {
            double height, weight;
            txtHeight.Text.Replace(".", ",");

            if (double.TryParse(txtHeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out height)
                && double.TryParse(txtWeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out weight))
            {
                _imc = weight/(height*height);
                lblImc.Text = _imc.ToString("0.##", CultureInfo.InvariantCulture);
            }
            else
                lblImc.Text = "";
        }
    }
}
