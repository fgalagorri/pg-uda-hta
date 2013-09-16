using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    public class HighlightSystolicCells : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var controller = GatewayController.GetInstance();
            var lim = controller.GetLimits();

            SolidColorBrush brush = new SolidColorBrush();
            int cellValue;

            int sys = 0;

            if (sys > lim.MaxSysNight)
            {
                return new SolidColorBrush(Colors.IndianRed);
            }

            return SystemColors.AppWorkspaceColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
