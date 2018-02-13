namespace Fidl.ViewModels.Interfaces
{
    using Caliburn.Micro;

    internal interface ITabConductorViewModel : IViewModelBase, IConductor
    {
        void SwitchTab();
    }
}