namespace Fidl.ViewModels.Dialogs
{
    using Fidl.ViewModels.Dialogs.Interfaces;

    internal class ErrorDialogViewModel : ViewModelBase, IErrorDialogViewModel
    {
        private string _message;
        public string Message
        {
            get => _message;

            set
            {
                if (_message == value) return;

                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public void Ok()
        {
            TryClose();
        }
    }
}