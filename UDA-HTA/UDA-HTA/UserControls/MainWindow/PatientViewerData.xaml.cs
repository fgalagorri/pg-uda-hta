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
    /// Interaction logic for PatientViewerData.xaml
    /// </summary>
    public partial class PatientViewerData : UserControl
    {
        public PatientViewerData()
        {
            InitializeComponent();

            List<ExampleMedition> l = new List<ExampleMedition>();
            ExampleMedition em = new ExampleMedition
            {
                Date = DateTime.Now,
                Sistolica = 142,
                Diastolica = 80,
                Media = 97,
                Freq = 66,
            };
            l.Add(em); l.Add(em); l.Add(em); l.Add(em); l.Add(em); l.Add(em);
            l.Add(em); l.Add(em); l.Add(em); l.Add(em); l.Add(em); l.Add(em);

            grid.DataContext = l;
        }
    }

    public class ExampleMedition
    {
        public DateTime Date { get; set; }
        public int Sistolica { get; set; }
        public int Diastolica { get; set; }
        public int Media { get; set; }
        public int Freq { get; set; }
    }
}
