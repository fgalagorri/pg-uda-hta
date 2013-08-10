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
    /// Interaction logic for PatientViewerOtherInfo.xaml
    /// </summary>
    public partial class PatientViewerOtherInfo : UserControl
    {
        public PatientViewerOtherInfo()
        {
            InitializeComponent();
        }

        public void SetInfo(ICollection<Effort> effort, ICollection<Complication> sintoms)
        {
            grEffort.DataContext = effort;
            grSintoms.DataContext = sintoms;
        }
    }
}
