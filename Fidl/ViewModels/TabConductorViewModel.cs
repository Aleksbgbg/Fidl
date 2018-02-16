namespace Fidl.ViewModels
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;
    using Fidl.ViewModels.Tabs.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;
    using Fidl.ViewModels.Tabs.Start.Interfaces;

    internal class TabConductorViewModel : Conductor<ITabViewModel>.Collection.OneActive, ITabConductorViewModel
    {
        public TabConductorViewModel(
                IStartViewModel startViewModel,
                IDriveManagerViewModel driveManagerViewModel,
                IRegistryEditorViewModel registryEditorViewModel
                )
        {
            ScreenExtensions.TryActivate(this);

            Items.Add(startViewModel);
            Items.Add(driveManagerViewModel);
            Items.Add(registryEditorViewModel);

            Items.Apply(tab => tab.Navigated += (sender, e) => ChangeActiveItem(tab, false));
        }

        public void SwitchTab()
        {
            ChangeActiveItem(Items[(Items.IndexOf(ActiveItem) + 1) % Items.Count], false);
        }
    }
}