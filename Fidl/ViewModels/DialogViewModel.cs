namespace Fidl.ViewModels
{
    using Fidl.ViewModels.Interfaces;

    internal class DialogViewModel : ViewModelBase, IDialogViewModel
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