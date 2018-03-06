using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;

namespace InstallerAppForms.MarkupExtension
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueAsString = value.ToString();
            switch (valueAsString)
            {
                case (""): { return Color.Default; }
                case ("Accent"): { return Color.Accent; }
                default: { return Color.FromHex(value.ToString()); }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
    }
}
