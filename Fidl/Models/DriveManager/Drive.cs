namespace Fidl.Models.DriveManager
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Caliburn.Micro;

    using FileSystemType = Fidl.Models.DriveManager.FileSystem;

    internal class Drive : PropertyChangedBase
    {
        private DriveInfo _driveInfo;

        internal Drive(DriveInfo driveInfo)
        {
            Path = driveInfo.Name;
            Update(driveInfo);
        }

        internal event EventHandler Updated;

        public string Path { get; }

        private string _name;
        public string Name
        {
            get => _name;

            private set
            {
                if (_name == value) return;

                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private DriveType _type;
        public DriveType Type
        {
            get => _type;

            private set
            {
                if (_type == value) return;

                _type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }

        private string _fileSystem;
        public string FileSystem
        {
            get => _fileSystem;

            private set
            {
                if (_fileSystem == value) return;

                _fileSystem = value;
                NotifyOfPropertyChange(() => FileSystem);
            }
        }

        private FileSystemType _fileSystemType;
        public FileSystemType FileSystemType
        {
            get => _fileSystemType;

            private set
            {
                if (_fileSystemType == value) return;

                _fileSystemType = value;
                NotifyOfPropertyChange(() => FileSystemType);
            }
        }

        private long _usedSpace;
        public long UsedSpace
        {
            get => _usedSpace;

            private set
            {
                if (_usedSpace == value) return;

                _usedSpace = value;
                NotifyOfPropertyChange(() => UsedSpace);
                NotifyOfPropertyChange(() => UsedSpacePercentage);
            }
        }

        private long _freeSpace;
        public long FreeSpace
        {
            get => _freeSpace;

            private set
            {
                if (_freeSpace == value) return;

                _freeSpace = value;
                NotifyOfPropertyChange(() => FreeSpace);
            }
        }

        private long _totalSize;
        public long TotalSize
        {
            get => _totalSize;

            private set
            {
                if (_totalSize == value) return;

                _totalSize = value;
                NotifyOfPropertyChange(() => TotalSize);
                NotifyOfPropertyChange(() => UsedSpacePercentage);
            }
        }

        private double _usedSpacePercentage;
        public double UsedSpacePercentage
        {
            get => _usedSpacePercentage;

            private set
            {
                if (_usedSpacePercentage == value) return;

                _usedSpacePercentage = value;
                NotifyOfPropertyChange(() => UsedSpacePercentage);
            }
        }

        internal void Update()
        {
            Update(new DriveInfo(Path));
        }

        internal void Update(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;

            Debug.Assert(driveInfo.IsReady, "Drive is not ready.", "Drives must be ready in order to be used.");
            Debug.Assert(driveInfo.VolumeLabel != null, "Drive is empty (e.g. CD-ROM).", "Drives must have content in order to be used.");

            Name = driveInfo.VolumeLabel;

            Type = driveInfo.DriveType;
            FileSystem = driveInfo.DriveFormat;
            if (FileSystem == "NTFS")
            {
                FileSystemType = FileSystemType.NTFS;
            }
            else if (FileSystem.Contains("FAT"))
            {
                FileSystemType = FileSystemType.FAT;
            }
            else
            {
                FileSystemType = FileSystemType.Other;
            }

            TotalSize = driveInfo.TotalSize;

            FreeSpace = driveInfo.AvailableFreeSpace;
            UsedSpace = TotalSize - FreeSpace;

            UsedSpacePercentage = (double)UsedSpace / TotalSize * 100;

            Updated?.Invoke(this, EventArgs.Empty);
        }

        internal void UpdateVolumeLabel(string newVolumeLabel)
        {
            _driveInfo.VolumeLabel = newVolumeLabel;
        }
    }
}