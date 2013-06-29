using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Entities;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewerInformation.xaml
    /// </summary>
    public partial class PatientViewerInformation : UserControl
    {
        public PatientViewerInformation()
        {
            InitializeComponent();

            List<ExampleMedication> l = new List<ExampleMedication>();
            ExampleMedication em = new ExampleMedication
            {
                Categoria = "Betabloqueante",
                Nombre = "Losartan",
                Principio = "Losartan",
                Dosis = "50 mg/dia"
            };
            l.Add(em); l.Add(em); l.Add(em);
            grMedication.DataContext = l;

            List<ExampleBackground> b = new List<ExampleBackground>();
            ExampleBackground back = new ExampleBackground
            {
                Enfermedad = "Bronquitis crónica",
                Comentario = "Hace 10 años",
            };
            b.Add(back); b.Add(back); b.Add(back);
            grBackground.DataContext = b;
        }


        public void SetPatientInfo(Patient patient)
        {
            var tempData = patient.LastTempData;

            // Identificación
            lblNames.Text = patient.Names;
            lblLast.Text = patient.Surnames;
            lblCI.Text = patient.DocumentId;
            lblBirth.Text = patient.BirthDate.Value.ToShortDateString();
            lblAge.Text = patient.BirthDate.Value.CalculateAge().ToString();
            lblSex.Text = patient.Sex.Value == SexType.M ? "Masculino" : "Femenino";

            // Condición
            if (tempData != null)
            {
                lblWeight.Text = tempData.Weight.ToString();
                lblHeight.Text = tempData.Height.ToString();
                lblImc.Text = tempData.BodyMassIndex.ToString();
                lblFat.Text = tempData.FatPercentage.ToString();
                lblMuscle.Text = tempData.MusclePercentage.ToString();
                lblKcal.Text = tempData.Kcal.ToString();
                lblSmoker.Text = tempData.Smoker.Value ? "Si" : "No";
                lblDiabetes.Text = tempData.Diabetic.Value ? "Si" : "No";
                lblColesterol.Text = tempData.Dyslipidemia.Value ? "Si" : "No";
                lblHypertense.Text = tempData.Hypertensive.Value ? "Si" : "No";
            }

            // Dirección
            lblAddress.Text = patient.Address;
            lblNeighbour.Text = patient.Neighbour;
            lblCity.Text = patient.City;
            lblDepartament.Text = patient.Department;
            lblTel.Text = patient.Phone;
            lblCel.Text = patient.CellPhone;
            lblTelAlt.Text = "Ver...";
            lblMail.Text = patient.Email;
        }

    }




    /* PROVISORIO */ // TODO sacar esto para otra dll que tenga las entidades
    public class ExampleMedication
    {
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string Principio { get; set; }
        public string Dosis { get; set; }
    }

    public class ExampleBackground
    {
        public string Enfermedad { get; set; }
        public string Comentario { get; set; }
    }
}
