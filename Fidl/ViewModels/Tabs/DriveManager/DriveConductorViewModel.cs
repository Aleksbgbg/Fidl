namespace Fidl.ViewModels.Tabs.DriveManager
{
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.DriveManager;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    internal class DriveConductorViewModel : Conductor<IDriveViewModel>.Collection.OneActive, IDriveConductorViewModel
    {
        private readonly IDriveFactory _driveFactory;

        public DriveConductorViewModel(IDriveFactory driveFactory)
        {
            _driveFactory = driveFactory;

            Items.AddRange(DriveInfo.GetDrives()
                                    .Where(driveInfo => driveInfo.IsReady && driveInfo.VolumeLabel != null)
                                    .Select(driveInfo => new Drive(driveInfo))
                                    .Select(_driveFactory.MakeDrive));

            CollectionViewSource.GetDefaultView(Items)
                                .SortDescriptions
                                .Add(new SortDescription(string.Join(".", nameof(Drive), nameof(Drive.Path)), ListSortDirection.Ascending));

            void ActivateInitialItem()
            {
                ActivateItem(Items.First());
            }

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ActiveItem) && ActiveItem == null && Items.Count != 0)
                {
                    ActivateInitialItem();
                }
            };

            Items.CollectionChanged += (sender, e) =>
            {
                if (ActiveItem != null || Items.Count == 0) return;

                ActivateInitialItem();
            };

            ActivateInitialItem();
        }

        public void RefreshDrives()
        {
            Items.Where(driveViewModel => !Directory.Exists(driveViewModel.Drive.Path)).ToArray().Apply(driveViewModel => driveViewModel.TryClose());

            // Update drives after removing old drives and before adding new ones, for least processing required
            Items.Apply(driveViewModel => driveViewModel.Drive.Update());

            string[] availableDrives = Items.Select(driveViewModel => driveViewModel.Drive.Path).ToArray();

            Items.AddRange(Directory.GetLogicalDrives()
                                    .Where(drive => !availableDrives.Contains(drive))
                                    .Select(driveLetter => new DriveInfo(driveLetter))
                                    .Where(driveInfo => driveInfo.IsReady && driveInfo.VolumeLabel != null)
                                    .Select(driveInfo => new Drive(driveInfo))
                                    .Select(_driveFactory.MakeDrive));
        }
    }
}