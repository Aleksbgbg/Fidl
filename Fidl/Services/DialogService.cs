namespace Fidl.Services
{
    using Caliburn.Micro;

    using Fidl.Services.Interfaces;
    using Fidl.ViewModels.Dialogs.Interfaces;
    using Fidl.ViewModels.Interfaces;

    internal class DialogService : IDialogService
    {
        private readonly IWindowManager _windowManager;

        public DialogService(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public void ShowDialog<TDialogViewModel>() where TDialogViewModel : IViewModelBase
        {
            ShowDialog(IoC.Get<TDialogViewModel>());
        }

        public void ShowDialog<TDialogViewModel>(TDialogViewModel dialogViewModel) where TDialogViewModel : IViewModelBase
        {
            _windowManager.ShowDialog(dialogViewModel);
        }

        public void ShowErrorDialog(string title, string message)
        {
            IErrorDialogViewModel errorDialogViewModel = IoC.Get<IErrorDialogViewModel>();
            errorDialogViewModel.DisplayName = title;
            errorDialogViewModel.Message = message;

            _windowManager.ShowDialog(errorDialogViewModel);
        }
    }
}