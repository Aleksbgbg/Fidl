namespace Fidl.ViewModels.Tabs.DriveManager.Interfaces
{
    using Fidl.ViewModels.Tabs.Interfaces;

    internal interface IDriveManagerViewModel : ITabViewModel
    {
        IDriveConductorViewModel DriveConductorViewModel { get; }
    }
}