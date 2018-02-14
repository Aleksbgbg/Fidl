namespace Fidl.ViewModels.Tabs.DriveManager
{
    using System.IO;

    using Fidl.Helpers.DriveManager;
    using Fidl.Models.DriveManager;
    using Fidl.Services.Interfaces;
    using Fidl.Utilities.Interfaces;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    using Microsoft.Win32;

    internal class DriveViewModel : ViewModelBase, IDriveViewModel
    {
        private readonly IApplicationInfo _applicationInfo;

        private readonly IDialogService _dialogService;

        private readonly IDriveIconService _driveIconService;

        private FileSystemNamingConvention _fileSystemNamingConvention;

        public DriveViewModel(IApplicationInfo applicationInfo, IDialogService dialogService, IDriveIconService driveIconService)
        {
            _applicationInfo = applicationInfo;
            _dialogService = dialogService;
            _driveIconService = driveIconService;
        }

        public Drive Drive { get; private set; }

        public void Initialise(Drive drive)
        {
            Drive = drive;
            DisplayName = drive.Name;

            _fileSystemNamingConvention = new FileSystemNamingConvention(drive.FileSystemType);
        }

        //protected override void OnActivate()
        //{
        //    if (!Directory.Exists(Drive.Path)) return;
        //
        //    Drive.Update();
        //}

        public bool CanUpdateVolumeLabel(string newVolumeLabel)
        {
            return _applicationInfo.LaunchedAsAdministrator &&
                   newVolumeLabel != Drive.Name &&
                   _fileSystemNamingConvention.IsValidName(newVolumeLabel);
        }

        public void UpdateVolumeLabel(string newVolumeLabel)
        {
            try
            {
                Drive.UpdateVolumeLabel(newVolumeLabel);
            }
            catch (IOException e) when (e.HResult == -2_147_024_742) // Volume Label too long
            {
                // This clause also executes identically with other file system errors, e.g. invalid chracters in name
                // Consider making this more robust
                _dialogService.ShowDialog("Volume Label Too Long", e.Message);
                return;
            }

            Drive.Update();
        }

        public void SelectNewIcon()
        {
            OpenFileDialog iconDialog = new OpenFileDialog
            {
                    Filter = "Icon Files|*.ico",
                    Title = "Please select an icon."
            };

            if (!(iconDialog.ShowDialog() ?? false))
            {
                return;
            }

            _driveIconService.SetIcon(Drive.Path, iconDialog.FileName);

            Drive.Update();
        }

        public void RestoreDefaultIcon()
        {
            _driveIconService.RemoveIcon(Drive.Path);

            Drive.Update();
        }
    }
}