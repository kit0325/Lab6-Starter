namespace Lab6_Starter.Converters;

using System;
using System.Globalization;
using Microsoft.Maui.Controls; // or using Xamarin.Forms;

public class NumberToStarsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int number)
        {
            return new string('★', number);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
