namespace Fidl.ViewModels.Interfaces
{
    internal interface IShellViewModel : IViewModelBase
    {
        IMainViewModel MainViewModel { get; }
    }
}