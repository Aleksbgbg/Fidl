namespace Fidl.ViewModels
{
    using Fidl.ViewModels.Interfaces;

    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(ITabConductorViewModel tabConductorViewModel)
        {
            TabConductorViewModel = tabConductorViewModel;
        }

        public ITabConductorViewModel TabConductorViewModel { get; }
    }
}