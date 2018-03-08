namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    using Microsoft.Win32;

    internal class WordEditViewModel : ValueEditViewModel, IWordEditViewModel
    {
        private const int DecDwordMaxValueLength = 10;
        private const int DecQwordMaxValueLength = 20;

        private const int HexDwordMaxValueLength = 8;
        private const int HexQwordMaxValueLength = 16;

        private Base _wordBase = Base.Hex;
        public Base WordBase
        {
            get => _wordBase;

            set
            {
                if (_wordBase == value) return;

                _wordBase = value;
                NotifyOfPropertyChange(() => WordBase);

                PerformActionsByBase(new Dictionary<ActionByBase, Action>
                {
                        [ActionByBase.DecDword] = () => MaxInputValueLength = DecDwordMaxValueLength,
                        [ActionByBase.DecQword] = () => MaxInputValueLength = DecQwordMaxValueLength,

                        [ActionByBase.HexDword] = () => MaxInputValueLength = HexDwordMaxValueLength,
                        [ActionByBase.HexQword] = () => MaxInputValueLength = HexQwordMaxValueLength
                });
            }
        }

        private string _inputValue;
        public string InputValue
        {
            get => _inputValue;

            set
            {
                if (_inputValue == value) return;

                _inputValue = value;
                NotifyOfPropertyChange(() => InputValue);

                PerformActionsByBase(new Dictionary<ActionByBase, Action>
                {
                        [ActionByBase.DecDword] = () => Value.StoredValue = uint.Parse(_inputValue),
                        [ActionByBase.DecQword] = () => Value.StoredValue = ulong.Parse(_inputValue),

                        [ActionByBase.HexDword] = () => Value.StoredValue = uint.Parse(_inputValue, NumberStyles.HexNumber),
                        [ActionByBase.HexQword] = () => Value.StoredValue = ulong.Parse(_inputValue, NumberStyles.HexNumber)
                });
            }
        }

        private int _maxInputValueLength;
        public int MaxInputValueLength
        {
            get => _maxInputValueLength;

            private set
            {
                if (_maxInputValueLength == value) return;

                _maxInputValueLength = value;
                NotifyOfPropertyChange(() => MaxInputValueLength);
            }
        }

        // Aims to simplify repetitive code with functional programming
        private void PerformActionsByBase(IReadOnlyDictionary<ActionByBase, Action> actions)
        {
            void PerformWordActions(Action dwordAction, Action qwordAction)
            {
                switch (Value.Kind)
                {
                    case RegistryValueKind.DWord:
                        dwordAction();
                        break;

                    case RegistryValueKind.QWord:
                        qwordAction();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(Value.Kind), Value.Kind, "Value Kind should be either DWord or QWord");
                }
            }

            switch (WordBase)
            {
                case Base.Dec:
                    PerformWordActions(actions[ActionByBase.DecDword], actions[ActionByBase.DecQword]);
                    break;

                case Base.Hex:
                    PerformWordActions(actions[ActionByBase.HexDword], actions[ActionByBase.HexQword]);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(WordBase), WordBase, "WordBase should be either Dec or Hex.");
            }
        }

        private enum ActionByBase
        {
            DecDword = Base.Dec * 32,
            DecQword = Base.Dec * 64,

            HexDword = Base.Hex * 32,
            HexQword = Base.Hex * 64
        }
    }
}