namespace Fidl.ViewModels.Tabs.Start.Interfaces
{
    using Fidl.Utilities.Interfaces;
    using Fidl.ViewModels.Tabs.Interfaces;

    internal interface IStartViewModel : ITabViewModel
    {
        IApplicationInfo ApplicationInfo { get; }
    }
}