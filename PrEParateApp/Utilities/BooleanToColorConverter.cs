using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace PrEParateApp.Utilities
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool esDeUsuario)
            {
                return esDeUsuario ? Colors.LightGray : Colors.Red;  // Colores para los mensajes del usuario y del médico
            }
            return Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Converting from color to boolean is not supported.");
        }
    }
}
