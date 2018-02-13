namespace Fidl.ViewModels.Tabs
{
    using System;
    using System.Text.RegularExpressions;

    using Fidl.ViewModels.Tabs.Interfaces;

    internal abstract class TabViewModel : ViewModelBase, ITabViewModel
    {
        private protected TabViewModel(string iconName, string description)
        {
            DisplayName = Regex.Replace(GetType().Name.Replace("ViewModel", string.Empty), @"(\B[A-Z])", " $1");
            IconName = iconName;
            Description = description;
        }

        public event EventHandler Navigated;

        public string IconName { get; }

        public string Description { get; }

        public void Navigate()
        {
            Navigated?.Invoke(this, EventArgs.Empty);
        }
    }
}