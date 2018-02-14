namespace Fidl.Factories
{
    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.DriveManager;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    internal class DriveFactory : IDriveFactory
    {
        public IDriveViewModel MakeDrive(Drive drive)
        {
            IDriveViewModel driveViewModel = IoC.Get<IDriveViewModel>();
            driveViewModel.Initialise(drive);

            return driveViewModel;
        }
    }
}