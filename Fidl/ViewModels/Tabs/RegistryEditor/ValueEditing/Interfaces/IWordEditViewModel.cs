﻿namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces
{
    using Fidl.Models.RegistryEditor;

    internal interface IWordEditViewModel : IValueEditViewModel
    {
        Base WordBase { get; set; }
    }
}