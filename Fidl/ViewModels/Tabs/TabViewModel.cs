namespace Fidl.ViewModels.Tabs
{
    using Fidl.ViewModels.Tabs.Interfaces;

    internal abstract class TabViewModel : ViewModelBase, ITabViewModel
    {
        private protected TabViewModel()
        {
            DisplayName = GetType().Name.Replace("ViewModel", string.Empty);
        }

        private protected TabViewModel(string iconName) : this()
        {
            IconName = iconName;
        }

        public string IconName { get; }
    }
}