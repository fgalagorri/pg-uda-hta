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

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewerDiagnosis.xaml
    /// </summary>
    public partial class PatientViewerDiagnosis : UserControl
    {
        public PatientViewerDiagnosis()
        {
            InitializeComponent();
            TestData();
        }

        void TestData()
        {
            FlowDocument myFlowDoc = new FlowDocument();
            myFlowDoc.Blocks.Add(new Paragraph(new Run("Registro de 24 horas realizado en forma ambulatoria, " +
                                                       "con un total de 88 mediciones de presión arterial, de " +
                                                       "las cuales 51 (57%) es válido para su interpretación.")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("La medición de PA en consultorio: 127/89 mmHg.")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("Los promedios de Presión Arterial sistólica y diastólica " +
                                                       "durante todo el período de registro (124/81 mmHg) están por " +
                                                       "debajo del umbral de hipertensión para MAPA (<130/ <80 mmHg). " +
                                                       "Al analizar individualmente los intervalos de vigilia y sueño " +
                                                       "en este período mantiene cifras promedio en rango normal. " +
                                                       "El registro incluye un período de sueño nocturno de 01:30 a 07:00). " +
                                                       "Presenta descenso de la presión arterial tanto sistolica como " +
                                                       "diastólica durante el sueño, el cual es <10% para la presión " +
                                                       "sistolica: non-dipper.  En la interpretación de este resultado " +
                                                       "debe tenerse en cuenta que el paciente tuvo un sueño " +
                                                       "con interrupciones por nocturia.")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("La frecuencia cardíaca promedio es de 71 cpm, " +
                                                       "sin medicación cronotropica negativa.")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("No presentó episodios de hipotensión ni síntomas de " +
                                                       "alarma durante el período de registro.")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("En suma patrón de Presión Arterial Normal, con descenso " +
                                                       "(dipping nocturno) de presión arterial anormal durante " +
                                                       "período de  sueño con interrupciones.")));
            txtDiagnosis.Document = myFlowDoc;

            lblDoctor.Text = "José Boggia";
            lblDate.Text = "21/2/2012";
        }
    }
}
