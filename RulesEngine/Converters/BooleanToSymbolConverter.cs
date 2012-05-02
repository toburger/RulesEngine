using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RulesEngine.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as bool?;
            if (val == null)
                return "K";
            else if (val == true)
                return "J";
            else
                return "L";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
