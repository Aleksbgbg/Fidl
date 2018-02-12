namespace Fidl.ViewModels
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.Interfaces;

    internal class TabConductorViewModel : Conductor<ITabViewModel>.Collection.OneActive, ITabConductorViewModel
    {
    }
}