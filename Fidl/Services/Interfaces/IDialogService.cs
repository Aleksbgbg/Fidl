namespace Fidl.Services.Interfaces
{
    using Fidl.ViewModels.Interfaces;

    internal interface IDialogService
    {
        void ShowDialog<TDialogViewModel>() where TDialogViewModel : IViewModelBase;

        void ShowDialog<TDialogViewModel>(TDialogViewModel dialogViewModel) where TDialogViewModel : IViewModelBase;

        void ShowErrorDialog(string title, string message);
    }
}