namespace Fidl.ViewModels.Interfaces
{
    internal interface IMainViewModel : IViewModelBase
    {
        ITabConductorViewModel TabConductorViewModel { get; }
    }
}