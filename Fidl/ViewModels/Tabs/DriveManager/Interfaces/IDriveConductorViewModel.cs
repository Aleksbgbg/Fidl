namespace Fidl.ViewModels.Tabs.DriveManager.Interfaces
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Interfaces;

    internal interface IDriveConductorViewModel : IViewModelBase, IConductor
    {
        void RefreshDrives();
    }
}