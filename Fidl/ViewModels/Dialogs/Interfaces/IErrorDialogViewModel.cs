namespace Fidl.ViewModels.Dialogs.Interfaces
{
    using Fidl.ViewModels.Interfaces;

    internal interface IErrorDialogViewModel : IViewModelBase
    {
        string Message { get; set; }
    }
}