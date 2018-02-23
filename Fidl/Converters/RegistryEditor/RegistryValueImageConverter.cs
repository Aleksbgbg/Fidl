namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    using Microsoft.Win32;

    [ValueConversion(typeof(RegistryValueKind), typeof(BitmapSource))]
    internal class RegistryValueImageConverter : IValueConverter
    {
        public static RegistryValueImageConverter Default { get; } = new RegistryValueImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((RegistryValueKind)value)
            {
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                case RegistryValueKind.MultiString:
                    return Application.Current.FindResource("RegistryString");

                case RegistryValueKind.Binary:
                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                case RegistryValueKind.Unknown:
                case RegistryValueKind.None:
                    return Application.Current.FindResource("RegistryBinary");

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, "RegistryValueKind is not valid.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}