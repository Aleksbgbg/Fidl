namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    using Fidl.EventArgs.RegistryEditor;
    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.Services.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    using Microsoft.Win32;

    using Key = System.Windows.Input.Key;

    internal class ValueViewModel : ViewModelBase, IValueViewModel
    {
        private readonly IRegistryFactory _registryFactory;

        private readonly IDialogService _dialogService;

        public ValueViewModel(IRegistryFactory registryFactory, IDialogService dialogService)
        {
            _registryFactory = registryFactory;
            _dialogService = dialogService;
        }

        public event EventHandler<ValueDeletedEventArgs> Deleted;

        public Value Value { get; private set; }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;

            private set
            {
                if (_isRenaming == value) return;

                _isRenaming = value;
                NotifyOfPropertyChange(() => IsRenaming);
            }
        }

        private string _newName;
        public string NewName
        {
            get => _newName;

            set
            {
                if (_newName == value) return;

                _newName = value;
                NotifyOfPropertyChange(() => NewName);
            }
        }

        public void Initialise(Value value)
        {
            Value = value;
            NewName = Value.Name;
            DisplayName = Value.Name;
        }

        public void Modify()
        {
            switch (Value.Kind)
            {
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    {
                        IStringEditViewModel stringEditViewModel = _registryFactory.MakeValueEditViewModel<IStringEditViewModel>(Value.StoredValue);
                        _dialogService.ShowDialog(stringEditViewModel);
                        Value.StoredValue = stringEditViewModel.Value;
                    }
                    break;

                case RegistryValueKind.Binary:
                case RegistryValueKind.Unknown:
                case RegistryValueKind.None:
                    break;

                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                    break;

                case RegistryValueKind.MultiString:
                    break;
            }
        }

        public void Delete()
        {
            Value.Delete();
            Deleted?.Invoke(this, new ValueDeletedEventArgs(this));
        }

        public void StartRenaming()
        {
            IsRenaming = true;
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                StartRenaming();
            }
        }

        public void RenameBoxKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    IsRenaming = false;
                    NewName = Value.Name;
                    break;

                case Key.Enter:
                    Rename();
                    break;
            }
        }

        public void RenameBoxLostFocus()
        {
            Rename();
        }

        private void Rename()
        {
            if (!IsRenaming) return;

            IsRenaming = false;

            if (NewName == Value.Name) return;

            if (NewName == string.Empty || Value.RegistryKey.GetValueNames().Contains(NewName))
            {
                _dialogService.ShowErrorDialog("Invalid Name", "The entered value name is invalid.");
                return;
            }

            Value.Rename(NewName);
        }
    }
}