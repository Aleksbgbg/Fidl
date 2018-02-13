namespace Fidl.Factories.Interfaces
{
    using Fidl.Models.Tabs.DriveManager;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    internal interface IDriveFactory
    {
        IDriveViewModel MakeDrive(Drive drive);
    }
}