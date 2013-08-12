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

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewerCondition.xaml
    /// </summary>
    public partial class PatientViewerCondition : UserControl
    {
        public PatientViewerCondition()
        {
            InitializeComponent();
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

                grMedication.DataContext = tempData.Medication;
            }

            grBackground.DataContext = bak;
        }
    }
}
