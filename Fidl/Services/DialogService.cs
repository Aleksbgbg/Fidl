namespace Fidl.Services
{
    using Caliburn.Micro;

    using Fidl.Services.Interfaces;
    using Fidl.ViewModels.Interfaces;

    internal class DialogService : IDialogService
    {
        private readonly IWindowManager _windowManager;

        public DialogService(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public void ShowDialog(string title, string message)
        {
            IDialogViewModel dialogViewModel = IoC.Get<IDialogViewModel>();
            dialogViewModel.DisplayName = title;
            dialogViewModel.Message = message;

            _windowManager.ShowDialog(dialogViewModel);
        }
    }
}