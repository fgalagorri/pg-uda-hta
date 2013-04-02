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

namespace UDA_HTA.UserControls
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

        private int calculateAge(DatePicker d)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - d.SelectedDate.Value.Year;
            if (d.SelectedDate > now.AddYears(-age)) age--;
            return age;
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
