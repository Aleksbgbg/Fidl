namespace Fidl.ViewModels.Tabs.DriveManager
{
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    internal class DriveManagerViewModel : TabViewModel, IDriveManagerViewModel
    {
        public DriveManagerViewModel(IDriveConductorViewModel driveConductorViewModel) : base("HardDrive", "View and configure various mounted drives.")
        {
            DriveConductorViewModel = driveConductorViewModel;
        }

        public IDriveConductorViewModel DriveConductorViewModel { get; }
    }
}