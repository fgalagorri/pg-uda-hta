using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UDA_HTA.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateInt(this TextBox txt, int minVal, int maxVal)
        {
            int intVal;
            bool canParse = int.TryParse(txt.Text, out intVal);

            if (!canParse || intVal > maxVal || intVal < minVal)
            {
                txt.BorderBrush = Brushes.Red;
                return false;
            }
            txt.ClearValue(Border.BorderBrushProperty);
            return true;
        }

        public static bool ValidateDecimal(this TextBox txt, decimal minVal, decimal maxVal)
        {
            decimal val;
            bool canParse = decimal.TryParse(txt.Text.Replace(",", "."), NumberStyles.Float,
                                             CultureInfo.InvariantCulture, out val);

            if (!canParse || val > maxVal || val < minVal)
            {
                txt.BorderBrush = Brushes.Red;
                return false;
            }
            txt.ClearValue(Border.BorderBrushProperty);
            return true;
        }

        public static bool ValidateString(this TextBox txt, bool required = true)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.BorderBrush = required? Brushes.Red : Brushes.Yellow;
                return false;
            }
            txt.ClearValue(Border.BorderBrushProperty);
            return true;
        }

        public static bool ValidateSelected(this ComboBox cmb)
        {
            if (cmb.SelectedIndex < 0)
            {
                cmb.BorderBrush = Brushes.Red;
                return false;
            }
            cmb.ClearValue(Border.BorderBrushProperty);
            return true;
        }

        public static bool ValidateDate(this DatePicker dt)
        {
            if (!dt.SelectedDate.HasValue)
            {
                dt.BorderBrush = Brushes.Red;
                return false;
            }
            dt.ClearValue(Border.BorderBrushProperty);
            return true;
        }
    }
}
