namespace Fidl.ViewModels.Tabs.DriveManager
{
    using System.IO;

    using Fidl.Helpers.DriveManager;
    using Fidl.Models.Tabs.DriveManager;
    using Fidl.Services.Interfaces;
    using Fidl.Utilities.Interfaces;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;

    using IniParser;
    using IniParser.Model;

    using Microsoft.Win32;

    internal class DriveViewModel : ViewModelBase, IDriveViewModel
    {
        private readonly IApplicationInfo _applicationInfo;

        private readonly IDialogService _dialogService;

        private FileSystemNamingConvention _fileSystemNamingConvention;

        public DriveViewModel(IApplicationInfo applicationInfo, IDialogService dialogService)
        {
            _applicationInfo = applicationInfo;
            _dialogService = dialogService;
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

        // TODO: Fix UnauthorisedAccessException errors in SelectNewIcon / RestoreDefaultIcon

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

            string driveIconDirectory = Path.Combine(Drive.Path, "Drive Icon");

            Directory.CreateDirectory(driveIconDirectory);

            string driveIconFile = Path.Combine(driveIconDirectory, "Drive Icon.ico");

            File.Delete(driveIconFile);
            File.Copy(iconDialog.FileName, driveIconFile, true);

            string autorunPath = Path.Combine(Drive.Path, "autorun.inf");

            if (!File.Exists(autorunPath))
            {
                File.WriteAllText(autorunPath, string.Empty);
            }

            // Consider expanding this functionality into Ini service
            {
                const string autorunString = "AUTORUN";
                const string iconString = "ICON";

                FileIniDataParser iniParser = new FileIniDataParser();
                IniData iniData = iniParser.ReadFile(autorunPath);

                if (!iniData.Sections.ContainsSection(autorunString))
                {
                    iniData.Sections.AddSection(autorunString);
                }

                KeyDataCollection autorunSection = iniData.Sections[autorunString];

                if (!autorunSection.ContainsKey(iconString))
                {
                    autorunSection.AddKey(iconString);
                }

                autorunSection.GetKeyData(iconString).Value = @"Drive Icon\Drive Icon.ico";

                iniParser.WriteFile(autorunPath, iniData);
            }

            foreach (string fileSystemEntry in new string[] { driveIconDirectory, driveIconFile, autorunPath })
            {
                File.SetAttributes(fileSystemEntry, FileAttributes.Hidden);
            }

            Drive.Update();
        }

        public void RestoreDefaultIcon()
        {
            string driveIconDirectory = Path.Combine(Drive.Path, "Drive Icon");

            if (Directory.Exists(driveIconDirectory))
            {
                Directory.Delete(driveIconDirectory, true);
            }

            // Consider expanding this functionality into Ini service
            const string autorunString = "AUTORUN";
            const string iconString = "ICON";

            string autorunPath = Path.Combine(Drive.Path, "autorun.inf");

            FileIniDataParser iniParser = new FileIniDataParser();
            IniData iniData = iniParser.ReadFile(autorunPath);

            if (iniData.Sections.ContainsSection(autorunString))
            {
                KeyDataCollection autorunSection = iniData.Sections[autorunString];

                if (autorunSection.ContainsKey(iconString))
                {
                    iniData.Sections[autorunString].RemoveKey(iconString);
                }

                if (iniData.Sections[autorunString].Count == 0)
                {
                    iniData.Sections.RemoveSection(autorunString);
                }
            }

            iniParser.WriteFile(autorunPath, iniData);

            if (File.ReadAllText(autorunPath) == string.Empty)
            {
                File.Delete(autorunPath);
            }

            Drive.Update();
        }
    }
}