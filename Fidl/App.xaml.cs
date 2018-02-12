namespace Fidl
{
    using System.Windows;

    public partial class App
    {
        public App()
        {
            Dispatcher.UnhandledException += (sender, e) =>
            {
                e.Handled = true;
                MessageBox.Show($"Operation unsucessful.\n\n{e.Exception.Message}", "An Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }
    }
}