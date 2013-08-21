using System;
using System.Collections;
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

namespace UDA_HTA.UserControls.MainWindow.Administration
{
    /// <summary>
    /// Interaction logic for AddDrugs.xaml
    /// </summary>
    public partial class AddDrugs : UserControl
    {
        public AddDrugs()
        {
            InitializeComponent();

            BindComboBox(comboBoxDrugType);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void BindComboBox(ComboBox comboBoxName)
        {
            //TODO obtner lista de tipos/categorias de medicamento
            IEnumerable list = new ArrayList(); // VER //
            comboBoxName.ItemsSource = list; 
//            comboBoxName.DisplayMemberPath = TIPO/CATEGORIA
//            comboBoxName.SelectedValuePath = IDENTIFICADOR
        }
    }
}
