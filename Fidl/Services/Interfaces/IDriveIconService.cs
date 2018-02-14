namespace Fidl.Services.Interfaces
{
    internal interface IDriveIconService
    {
        void SetIcon(string drivePath, string iconPath);

        void RemoveIcon(string drivePath);
    }
}