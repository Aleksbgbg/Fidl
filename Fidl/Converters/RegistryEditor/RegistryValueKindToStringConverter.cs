namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;
    using Microsoft.Win32;

    [ValueConversion(typeof(RegistryValueKind), typeof(string))]
    internal class RegistryValueKindToStringConverter : IValueConverter
    {
        public static RegistryValueKindToStringConverter Instance { get; } = new RegistryValueKindToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null");

            switch ((RegistryValueKind)value)
            {
                case RegistryValueKind.String:
                    return "String (REG_SZ)";

                case RegistryValueKind.ExpandString:
                    return "ExpandString (REG_EXPAND_SZ)";

                case RegistryValueKind.Binary:
                    return "Binary (REG_BINARY)";

                case RegistryValueKind.DWord:
                    return "DoubleWord (REG_DWORD)";

                case RegistryValueKind.MultiString:
                    return "Multi-String (REG_MULTI_SZ)";

                case RegistryValueKind.QWord:
                    return "QuadWord (REG_QWORD)";

                case RegistryValueKind.Unknown:
                    return "Unknown (REG_UNKNOWN)";

                case RegistryValueKind.None:
                    return "None (REG_NONE)";

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}