using System.Collections.Generic;
using System.Configuration;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerCondition.xaml
    /// </summary>
    public partial class PatientViewerCondition : UserControl
    {
        public PatientViewerCondition()
        {
            InitializeComponent();
            ColHora.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
        }


        public void SetInfo(TemporaryData tempData, ICollection<MedicalRecord> bak)
        {
            if (tempData != null)
            {
                lblWeight.Text = tempData.Weight.ToString();
                lblHeight.Text = tempData.Height.ToString();
                lblImc.Text = tempData.BodyMassIndex.ToString();
                lblFat.Text = tempData.FatPercentage.ToString();
                lblMuscle.Text = tempData.MusclePercentage.ToString();
                lblKcal.Text = tempData.Kcal.ToString();
                lblSmoker.Text = tempData.Smoker != null && tempData.Smoker.Value ? "Si" : "No";
                lblDiabetes.Text = tempData.Diabetic != null && tempData.Diabetic.Value ? "Si" : "No";
                lblColesterol.Text = tempData.Dyslipidemia != null && tempData.Dyslipidemia.Value ? "Si" : "No";
                lblHypertense.Text = tempData.Hypertensive != null && tempData.Hypertensive.Value ? "Si" : "No";

                grMedication.DataContext = null;
                grMedication.DataContext = tempData.Medication;
            }

            grBackground.DataContext = null;
            grBackground.DataContext = bak;
        }
    }
}
