namespace Fidl.ViewModels
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;
    using Fidl.ViewModels.Tabs.Interfaces;
    using Fidl.ViewModels.Tabs.Start.Interfaces;

    internal class TabConductorViewModel : Conductor<ITabViewModel>.Collection.OneActive, ITabConductorViewModel
    {
        public TabConductorViewModel(
                IStartViewModel startViewModel,
                IDriveManagerViewModel driveManagerViewModel
                )
        {
            Items.Add(startViewModel);
            Items.Add(driveManagerViewModel);

            foreach (ITabViewModel tab in Items)
            {
                tab.Navigated += (sender, e) => ChangeActiveItem(tab, false);
            }
        }
    }
}