using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace UDA_HTA.UserControls.MainWindow
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
