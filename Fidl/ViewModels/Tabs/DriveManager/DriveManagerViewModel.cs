namespace Fidl.ViewModels.Tabs.DriveManager
{
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    internal class DriveManagerViewModel : TabViewModel, IDriveManagerViewModel
    {
        public DriveManagerViewModel() : base("HardDrive")
        {
        }
    }
}