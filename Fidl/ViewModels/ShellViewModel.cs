namespace Fidl.ViewModels
{
    using System.Windows.Input;

    using Fidl.Helpers;
    using Fidl.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IMainViewModel mainViewModel, ITabConductorViewModel tabConductorViewModel)
        {
            DisplayName = "Fidl";

            MainViewModel = mainViewModel;

            SwitchTabCommand = new RelayCommand<object>(parameter => tabConductorViewModel.SwitchTab());
        }

        public IMainViewModel MainViewModel { get; }

        public ICommand SwitchTabCommand { get; }
    }
}