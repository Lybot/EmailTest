using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace EmailTest.Models
{
    public class InputMultiConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string result=String.Empty;
            foreach (var stringValue in values)
            {
                result += stringValue.ToString() + " ";
            }
            return result;
            //Tuple<string, string> tuple = new Tuple<string, string>(
            //    (string)values[0], (string)values[1]);
            //return tuple;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            List<string> help = new List<string>();
            help.Add(value.ToString());
            return help.ToArray();
        }
    }
}
