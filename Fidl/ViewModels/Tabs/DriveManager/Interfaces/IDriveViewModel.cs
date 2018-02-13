namespace Fidl.ViewModels.Tabs.DriveManager.Interfaces
{
    using Fidl.Models.Tabs.DriveManager;
    using Fidl.ViewModels.Interfaces;

    internal interface IDriveViewModel : IViewModelBase
    {
        Drive Drive { get; }

        void Initialise(Drive drive);
    }
}