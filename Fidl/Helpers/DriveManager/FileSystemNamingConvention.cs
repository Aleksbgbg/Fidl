namespace Fidl.Helpers.DriveManager
{
    using System;
    using System.Linq;

    internal class FileSystemNamingConvention
    {
        private readonly Predicate<string> _isValidName;

        internal FileSystemNamingConvention(FileSystem fileSystem)
        {
            switch (fileSystem)
            {
                case FileSystem.NTFS:
                    _isValidName = name => name.Length <= 32;
                    break;

                case FileSystem.FAT:
                    _isValidName = name => name.Length <= 11 &&
                                           !name.Any("?/\\|.,;:+=[]<>\"\t".Contains) &&
                                           name.All(character => character <= 127);
                    break;

                case FileSystem.Other:
                    _isValidName = name => true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(fileSystem), fileSystem, "FileSystem out of range (not NTFS, FAT, or Other).");
            }
        }

        internal bool IsValidName(string name)
        {
            return _isValidName(name);
        }
    }
}