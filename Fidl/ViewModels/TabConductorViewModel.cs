namespace Fidl.ViewModels
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.Interfaces;
    using Fidl.ViewModels.Tabs.Start.Interfaces;

    internal class TabConductorViewModel : Conductor<ITabViewModel>.Collection.OneActive, ITabConductorViewModel
    {
        public TabConductorViewModel(
                IStartViewModel startViewModel
                )
        {
            Items.Add(startViewModel);
        }
    }
}