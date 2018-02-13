namespace Fidl.ViewModels
{
    using System.Windows.Input;

    using Fidl.Helpers;
    using Fidl.Utilities.Interfaces;
    using Fidl.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IMainViewModel mainViewModel, ITabConductorViewModel tabConductorViewModel, IApplicationInfo applicationInfo)
        {
            DisplayName = applicationInfo.LaunchedAsAdministrator ? "Fidl" : "Fidl (Administrator)";

            MainViewModel = mainViewModel;

            SwitchTabCommand = new RelayCommand<object>(parameter => tabConductorViewModel.SwitchTab());
        }

        public IMainViewModel MainViewModel { get; }

        public ICommand SwitchTabCommand { get; }
    }
}