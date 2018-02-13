namespace Fidl.ViewModels.Tabs.Interfaces
{
    using System;

    using Fidl.ViewModels.Interfaces;

    internal interface ITabViewModel : IViewModelBase
    {
        event EventHandler Navigated;

        string IconName { get; }

        string Description { get; }

        void Navigate();
    }
}