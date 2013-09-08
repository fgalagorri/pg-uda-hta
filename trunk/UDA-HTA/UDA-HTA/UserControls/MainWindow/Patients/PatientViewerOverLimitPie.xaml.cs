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

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerOverLimitPie.xaml
    /// </summary>
    public partial class PatientViewerOverLimitPie : UserControl
    {
        public PatientViewerOverLimitPie()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            int SysAceptableMin = 130;
            int SysAltoMin = 150;
            int DiasAceptableMin = 80;
            int DiasAltoMin = 100;

            var valid = r.Measures.Where(m => m.Valid && m.Asleep.HasValue
                                              && m.Systolic.HasValue
                                              && m.Diastolic.HasValue).ToList();

            PieSysTot.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Demasiado Alto",
                                                  valid.Count(m => m.Systolic.Value >= SysAltoMin)),
                    new KeyValuePair<string, int>("Aceptable",
                                                  valid.Count(m => m.Systolic.Value >= SysAceptableMin
                                                                   && m.Systolic.Value < SysAltoMin)),
                    new KeyValuePair<string, int>("Normal",
                                                  valid.Count(m => m.Systolic.Value < SysAceptableMin)),
                };

            PieSysDay.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Demasiado Alto",
                                                  valid.Count(m => !m.Asleep.Value
                                                                   && m.Systolic.Value >= SysAltoMin)),
                    new KeyValuePair<string, int>("Aceptable",
                                                  valid.Count(m => !m.Asleep.Value
                                                                   && m.Systolic.Value >= SysAceptableMin
                                                                   && m.Systolic.Value < SysAltoMin)),
                    new KeyValuePair<string, int>("Normal",
                                                  valid.Count(m => !m.Asleep.Value
                                                                   && m.Systolic.Value < SysAceptableMin)),
                };

            PieSysNight.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Demasiado Alto",
                                                  valid.Count(m => m.Asleep.Value
                                                                   && m.Systolic.Value >= SysAltoMin)),
                    new KeyValuePair<string, int>("Aceptable",
                                                  valid.Count(m => m.Asleep.Value
                                                                   && m.Systolic.Value >= SysAceptableMin
                                                                   && m.Systolic.Value < SysAltoMin)),
                    new KeyValuePair<string, int>("Normal",
                                                  valid.Count(m => m.Asleep.Value
                                                                   && m.Systolic.Value < SysAceptableMin)),
                };


            // Diastolic measures
            PieDiasTot.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Demasiado Alto",
                                                  valid.Count(m => m.Diastolic.Value >= DiasAltoMin)),
                    new KeyValuePair<string, int>("Aceptable",
                                                  valid.Count(m => m.Diastolic.Value >= DiasAceptableMin
                                                                   && m.Diastolic.Value < DiasAltoMin)),
                    new KeyValuePair<string, int>("Normal",
                                                  valid.Count(m => m.Diastolic.Value < DiasAceptableMin)),
                };

            PieDiasDay.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Demasiado Alto",
                                                  valid.Count(m => !m.Asleep.Value
                                                                   && m.Diastolic.Value >= DiasAltoMin)),
                    new KeyValuePair<string, int>("Aceptable",
                                                  valid.Count(m => !m.Asleep.Value
                                                                   && m.Diastolic.Value >= DiasAceptableMin
                                                                   && m.Diastolic.Value < DiasAltoMin)),
                    new KeyValuePair<string, int>("Normal",
                                                  valid.Count(m => !m.Asleep.Value
                                                                   && m.Diastolic.Value < DiasAceptableMin)),
                };

            PieDiasNight.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Demasiado Alto",
                                                  valid.Count(m => m.Asleep.Value
                                                                   && m.Diastolic.Value >= DiasAltoMin)),
                    new KeyValuePair<string, int>("Aceptable",
                                                  valid.Count(m => m.Asleep.Value
                                                                   && m.Diastolic.Value >= DiasAceptableMin
                                                                   && m.Diastolic.Value < DiasAltoMin)),
                    new KeyValuePair<string, int>("Normal",
                                                  valid.Count(m => m.Asleep.Value
                                                                   && m.Diastolic.Value < DiasAceptableMin)),
                };
        }
    }
}
