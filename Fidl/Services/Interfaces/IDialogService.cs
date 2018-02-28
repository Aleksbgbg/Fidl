namespace Fidl.Services.Interfaces
{
    internal interface IDialogService
    {
        void ShowDialog<TDialogViewModel>();

        void ShowErrorDialog(string title, string message);
    }
}