using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ExpressionsDemo
{
    public class FieldWrapper<T> : ObservableObject
    {
        private T _value;
        private bool _hasError;
        private bool _isEnabled;

        public FieldWrapper() { }
        public FieldWrapper(T value)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        public bool IsValid
        {
            get => !_hasError;
            set => SetProperty(ref _hasError, !value);
        }

        public bool IsEnabled
        {
            get => !_isEnabled;
            set => SetProperty(ref _isEnabled, !value);
        }

        public static implicit operator T (FieldWrapper<T> wrapper)
        {
            return wrapper.Value;
        }

        public static implicit operator FieldWrapper<T>(T value)
        {
            return new FieldWrapper<T>(value);
        }
    }
}
