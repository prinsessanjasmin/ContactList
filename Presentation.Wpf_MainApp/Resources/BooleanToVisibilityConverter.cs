﻿using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Presentation.Wpf_MainApp.Resources;

/// <summary>
/// This method is wholly generated by ChatGPT 4o. I wanted help with creating a solution for hiding the error fields when they don't have 
/// any messages to convey.Because the method inherits from the base class IValueConverter, both of the included methods are needed. In my
/// case I don't have a need for converting the bool value back to Visibility, which is why that method is not implemented. 
/// </summary>
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is bool boolValue && boolValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
