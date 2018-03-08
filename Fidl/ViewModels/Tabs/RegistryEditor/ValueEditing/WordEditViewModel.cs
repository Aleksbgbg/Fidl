namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using System;
    using System.Globalization;

    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal class WordEditViewModel : ValueEditViewModel, IWordEditViewModel
    {
        private const int HexDwordMaxValueLength = 8;
        private const int HexQwordMaxValueLength = 16;

        private const int DecDwordMaxValueLength = 10;
        private const int HexQwordMaxValueLength = 8;

        private Base _wordBase = Base.Hex;
        public Base WordBase
        {
            get => _wordBase;

            set
            {
                if (_wordBase == value) return;

                _wordBase = value;
                NotifyOfPropertyChange(() => WordBase);

                switch (_wordBase)
                {
                    case Base.Dec:
                        MaxInputValueLength = ;
                        break;

                    case Base.Hex:
                        MaxInputValueLength = ;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(WordBase), WordBase, "WordBase should be either Dec or Hex.");
                }
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

                switch (WordBase)
                {
                    case Base.Dec:
                        Value.StoredValue = int.Parse(_inputValue);
                        break;

                    case Base.Hex:
                        Value.StoredValue = int.Parse(_inputValue, NumberStyles.HexNumber);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(WordBase), WordBase, "WordBase should be either Dec or Hex.");
                }
            }
        }

        private int _maxInputValueLength;
        public int MaxInputValueLength
        {
            get => _maxInputValueLength;

            set
            {
                if (_maxInputValueLength == value) return;

                _maxInputValueLength = value;
                NotifyOfPropertyChange(() => MaxInputValueLength);
            }
        }
    }
}