using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ExpressionsDemo
{
    public class FormViewModel : ObservableObject
    {
        FieldWrapper<string> _field1 = string.Empty;
        FieldWrapper<string> _field2 = string.Empty;
        FieldWrapper<int> _field3 = 0;

        public FieldWrapper<string> Field1
        {
            get => _field1;
            set => SetProperty(ref _field1, value);
        }

        public FieldWrapper<string> Field2
        {
            get => _field2;
            set => SetProperty(ref _field2, value);
        }

        public FieldWrapper<int> Field3
        {
            get => _field3;
            set => SetProperty(ref _field3, value);
        }
    }
}
