namespace Fidl.Services.Interfaces
{
    internal interface IIniService
    {
        void AddKey(string filename, string section, string key, string value);

        void RemoveKey(string filename, string section, string key);
    }
}