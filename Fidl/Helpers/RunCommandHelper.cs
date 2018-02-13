namespace Fidl.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    internal static class RunCommandHelper
    {
        private static readonly DependencyProperty AttachRunCommandProperty = DependencyProperty.RegisterAttached("AttachRunCommand",
                                                                                                                  typeof(string),
                                                                                                                  typeof(RunCommandHelper),
                                                                                                                  new FrameworkPropertyMetadata(AttachRunCommandProperty_Changed));

        internal static string GetAttachRunCommand(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(AttachRunCommandProperty);
        }

        internal static void SetAttachRunCommand(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(AttachRunCommandProperty, value);
        }

        // Consider adding support for other clickable objects in the future (e.g. ButtonBase; MenuItem)
        private static void AttachRunCommandProperty_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is Button button))
            {
                Debug.Assert(dependencyObject is Button, "Non-button object attached run command.", "Only buttons should have this event attached.");
                throw new ArgumentOutOfRangeException(nameof(dependencyObject), dependencyObject, "DependencyObject must be Button.");
            }

            string newValueString = (string)e.NewValue;

            void ButtonOnClick(object sender, RoutedEventArgs args)
            {
                Process.Start(newValueString);
            }

            if (string.IsNullOrWhiteSpace(newValueString))
            {
                button.Click -= ButtonOnClick;
            }
            else
            {
                button.Click += ButtonOnClick;
            }
        }
    }
}