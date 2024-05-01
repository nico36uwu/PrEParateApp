using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace PrEParateApp.Utilities
{
    public class BooleanToLayoutOptionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool EsDeUsuario)
            {
                return EsDeUsuario ? LayoutOptions.End : LayoutOptions.Start;
            }
            return LayoutOptions.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
