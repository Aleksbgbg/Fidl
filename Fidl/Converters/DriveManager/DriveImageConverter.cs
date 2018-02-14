namespace Fidl.Converters.DriveManager
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    [ValueConversion(typeof(string), typeof(BitmapSource))]
    internal class DriveImageConverter : IValueConverter
    {
        private const int ImageRetrievalFlags = 0x100; // Flag 0x100 represents 'SHGFI_ICON' (retrieve the handle to the icon that represents the file)

        public static DriveImageConverter Instance { get; } = new DriveImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ShFileInfo shFileInfo = new ShFileInfo();

            SHGetFileInfo((string)value, 0, ref shFileInfo, (uint)Marshal.SizeOf(shFileInfo), ImageRetrievalFlags);

            return Imaging.CreateBitmapSourceFromHBitmap(Icon.FromHandle(shFileInfo.iconHandle)
                                                             .ToBitmap()
                                                             .GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string filePath, uint fileAttributes, ref ShFileInfo shFileInfoPointer, uint imagePointerSize, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        private struct ShFileInfo
        {
            public IntPtr iconHandle;

            public int iconIndex;

            public uint fileAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string fileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string fileType;
        }

    }
}