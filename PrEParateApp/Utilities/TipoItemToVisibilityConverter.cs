using PrEParateApp.ViewModel;
using System;
using System.Globalization;

namespace PrEParateApp.Utilities
{
    public class TipoItemToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == Constantes.TOMA_MEDICACION;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
