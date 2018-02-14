namespace Fidl.Factories.Interfaces
{
    using Fidl.Models.DriveManager;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    internal interface IDriveFactory
    {
        IDriveViewModel MakeDrive(Drive drive);
    }
}