using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Assignment.Views
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 1:
                    return new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
                case 2:
                    return new SolidColorBrush(Color.FromArgb(40, 255, 165, 0));
                case 3:
                    return new SolidColorBrush(Color.FromArgb(40, 0, 128, 0));
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
