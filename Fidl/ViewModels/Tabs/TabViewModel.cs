namespace Fidl.ViewModels.Tabs
{
    using System;
    using System.Text.RegularExpressions;

    using Fidl.ViewModels.Tabs.Interfaces;

    internal abstract class TabViewModel : ViewModelBase, ITabViewModel
    {
        private protected TabViewModel(string iconName)
        {
            DisplayName = Regex.Replace(GetType().Name.Replace("ViewModel", string.Empty), @"(\B[A-Z])", " $1");
            IconName = iconName;
        }

        public event EventHandler Navigated;

        public string IconName { get; }

        public void Navigate()
        {
            Navigated?.Invoke(this, EventArgs.Empty);
        }
    }
}