namespace Fidl.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal static class ImageHelper
    {
        private static readonly DependencyProperty AttachImageProperty = DependencyProperty.RegisterAttached("AttachImage",
                                                                                                             typeof(ImageSource),
                                                                                                             typeof(ImageHelper),
                                                                                                             new FrameworkPropertyMetadata(AttachImageProperty_Changed));

        internal static ImageSource GetAttachImage(DependencyObject dependencyObject)
        {
            return (ImageSource)dependencyObject.GetValue(AttachImageProperty);
        }

        internal static void SetAttachImage(DependencyObject dependencyObject, BitmapImage value)
        {
            dependencyObject.SetValue(AttachImageProperty, value);
        }

        private static void AttachImageProperty_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            switch (dependencyObject)
            {
                case Button button:
                    {
                        Debug.Assert(button.Content is string, "Image added on button with content other than string.");

                        StackPanel buttonStackPanel = new StackPanel { Orientation = Orientation.Horizontal };

                        buttonStackPanel.Children.Add(new Image { Source = (ImageSource)e.NewValue, Height = 20 });
                        buttonStackPanel.Children.Add(new TextBlock { Text = (string)button.Content, Margin = new Thickness(5, 0, 5, 0) });

                        button.Content = buttonStackPanel;
                    }
                    break;

                case MenuItem menuItem:
                    menuItem.Icon = new Image { Source = (ImageSource)e.NewValue };
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dependencyObject), dependencyObject, "Image-attached object must have an image attachment implementation.");
            }
        }
    }
}