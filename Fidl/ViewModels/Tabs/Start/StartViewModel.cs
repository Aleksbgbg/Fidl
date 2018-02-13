namespace Fidl.ViewModels.Tabs.Start
{
    using Fidl.Utilities.Interfaces;
    using Fidl.ViewModels.Tabs.Start.Interfaces;

    internal class StartViewModel : TabViewModel, IStartViewModel
    {
        public StartViewModel(IApplicationInfo applicationInfo) : base("Logo", "Welcome to Fidl!")
        {
            ApplicationInfo = applicationInfo;
        }

        public IApplicationInfo ApplicationInfo { get; }
    }
}