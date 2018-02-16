namespace Fidl.ViewModels.Tabs.DriveManager
{
    using System;
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

        private DateTime _lastDriveUpdateTime;

        public DriveViewModel(IApplicationInfo applicationInfo, IDialogService dialogService, IDriveIconService driveIconService)
        {
            _applicationInfo = applicationInfo;
            _dialogService = dialogService;
            _driveIconService = driveIconService;
        }

        private string _driveRenameDisabledMessage;
        public string DriveRenameDisabledMessage
        {
            get => _driveRenameDisabledMessage;

            private set
            {
                if (_driveRenameDisabledMessage == value) return;

                _driveRenameDisabledMessage = value;
                NotifyOfPropertyChange(() => DriveRenameDisabledMessage);
            }
        }

        public Drive Drive { get; private set; }

        public void Initialise(Drive drive)
        {
            drive.Updated += (sender, e) => _lastDriveUpdateTime = DateTime.Now;

            Drive = drive;
            DisplayName = drive.Name;

            _fileSystemNamingConvention = new FileSystemNamingConvention(drive.FileSystemType);

            CanUpdateVolumeLabel(Drive.Name);
        }

        protected override void OnActivate()
        {
            if ((DateTime.Now - _lastDriveUpdateTime).TotalMinutes <= 2) return;

            if (!Directory.Exists(Drive.Path)) return;

            Drive.Update();
        }

        public bool CanUpdateVolumeLabel(string newVolumeLabel)
        {
            if (!_applicationInfo.LaunchedAsAdministrator)
            {
                DriveRenameDisabledMessage = "Fidl does not have administrator privileges.";
                return false;
            }

            if (newVolumeLabel == Drive.Name)
            {
                DriveRenameDisabledMessage = "Volume label is already set to the provided value.";
                return false;
            }

            if (!_fileSystemNamingConvention.IsValidName(newVolumeLabel))
            {
                DriveRenameDisabledMessage = "Invalid volume label for the target file system.";
                return false;
            }

            return true;
        }

        public void UpdateVolumeLabel(string newVolumeLabel)
        {
            try
            {
                Drive.UpdateVolumeLabel(newVolumeLabel);
            }
            catch (IOException e) when (e.HResult == -2_147_024_742) // Volume Label errors
            {
                // Originally intended to provide a Volume Label Too Long error, however it was later discovered that the same HResult
                // and exception messages are provided with any volume label naming errors, so a general 'Invalid Volume Label'
                // message is provided instead.
                _dialogService.ShowDialog("Invalid Volume Label",
                                          "The provided volume label is not appropriate to the target file system.\n\nAn error " +
                                         $"message is provided with this error (however may not be accurate):\n\n'{e.Message}'");
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