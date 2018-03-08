namespace Fidl.Converters.RegistryEditor.ValueEditing
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Fidl.Models.RegistryEditor;

    [ValueConversion(typeof(Base), typeof(bool))]
    internal class BaseEnumConverter : IValueConverter
    {
        public static BaseEnumConverter Default { get; } = new BaseEnumConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(typeof(Base), (string)parameter).Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(typeof(Base), (string)parameter);
        }
    }
}