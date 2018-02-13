namespace Fidl.ViewModels
{
    using System.Windows;

    using Fidl.ViewModels.Interfaces;

    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(ITabConductorViewModel tabConductorViewModel)
        {
            TabConductorViewModel = tabConductorViewModel;
        }

        public ITabConductorViewModel TabConductorViewModel { get; }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}