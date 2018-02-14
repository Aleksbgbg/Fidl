namespace Fidl.Services
{
    using System;
    using System.IO;
    using System.Linq;

    using Fidl.Services.Interfaces;

    internal class DriveIconService : IDriveIconService
    {
        private const string IconDirectoryName = "Drive Icon";
        private const string IconFileName = "Drive Icon.ico";
        private const string AutorunFileName = "autorun.inf";

        private const string AutorunString = "AUTORUN";
        private const string IconString = "ICON";

        private readonly IIniService _iniService;

        public DriveIconService(IIniService iniService)
        {
            _iniService = iniService;
        }

        public void SetIcon(string drivePath, string iconPath)
        {
            ChangeIcon(drivePath, (iconDirectory, iconFile, autorunPath) =>
            {
                Directory.CreateDirectory(iconDirectory);

                File.Copy(iconPath, iconFile, true);

                // Relative icon path provided to prevent drive letter changes from affecting drive icon
                _iniService.AddKey(autorunPath, AutorunString, IconString, Path.Combine(Path.GetFileName(Path.GetDirectoryName(iconFile)), Path.GetFileName(iconFile)));
            });
        }

        public void RemoveIcon(string drivePath)
        {
            ChangeIcon(drivePath, (iconDirectory, iconFile, autorunPath) =>
            {
                if (File.Exists(iconFile))
                {
                    File.Delete(iconFile);
                }

                if (Directory.Exists(iconDirectory) && !Directory.EnumerateFileSystemEntries(iconDirectory).Any())
                {
                    Directory.Delete(iconDirectory);
                }

                _iniService.RemoveKey(autorunPath, AutorunString, IconString);
            });
        }

        private static void ChangeIcon(string drivePath, Action<string, string, string> method)
        {
            string iconDirectory = Path.Combine(drivePath, IconDirectoryName);
            string iconFile = Path.Combine(iconDirectory, IconFileName);
            string autorunPath = Path.Combine(drivePath, AutorunFileName);

            string[] fileSystemEntries = { iconDirectory, iconFile, autorunPath };

            void SetAttributes(FileAttributes fileAttributes)
            {
                foreach (string fileSystemEntry in fileSystemEntries.Where(fileSystemEntry => File.Exists(fileSystemEntry) || Directory.Exists(fileSystemEntry)))
                {
                    File.SetAttributes(fileSystemEntry, fileAttributes);
                }
            }

            SetAttributes(FileAttributes.Normal); // Prevent errors when deleting/overwriting of files/folders

            method(iconDirectory, iconFile, autorunPath);

            SetAttributes(FileAttributes.Hidden); // Hide files to represent significance to the system
        }
    }
}