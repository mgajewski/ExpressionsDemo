using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ExpressionsDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isInitialized = false;
        private LogicExpressions _evaluator;
        private ScriptEvaluator _scriptEvaluator = new ScriptEvaluator();
        private FormViewModel _formVM = new FormViewModel();
        
        public MainWindow()
        {
            InitializeComponent();

            validationField1.Text = "Field1.Value.Length >= 4";
            validationField2.Text = "Field2 = \"Doe\"";
            validationField3.Text = "(Field3 >= Field2.Value.Length) AND (Field3 < Field2.Value.Length + 2)";

            enabledField1.Text = "true";
            enabledField2.Text = "Field1 = \"Jhon\" OR Field1 = \"Stefan\"";
            enabledField3.Text = "Field1 = \"Jhon\" AND Field2 = \"Doe\"";

            var variables = new Dictionary<string, object>()
            {
                { nameof(_formVM.Field1), _formVM.Field1 },
                { nameof(_formVM.Field2), _formVM.Field2 },
                { nameof(_formVM.Field3), _formVM.Field3 },
            };

            _evaluator = new LogicExpressions(variables);

            _isInitialized = true;
            EvaluateEnabledExpressions();
        }

        public FormViewModel FormVM => _formVM;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EvaluateValidationExpressions();
            EvaluateEnabledExpressions();
        }

        private void field_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluateValidationExpressions();
            EvaluateEnabledExpressions();
        }

        private void EvaluateValidationExpressions()
        {
            if (_isInitialized)
            {
                FormVM.Field1.HasError = !EvaluateValidation(validationField1.Text);
                FormVM.Field2.HasError = !EvaluateValidation(validationField2.Text);
                FormVM.Field3.HasError = !EvaluateValidation(validationField3.Text);
            }
        }

        private void EvaluateEnabledExpressions()
        {
            if (_isInitialized)
            {
                FormVM.Field1.IsEnabled = EvaluateEnabled(enabledField1.Text);
                FormVM.Field2.IsEnabled = EvaluateEnabled(enabledField2.Text);
                FormVM.Field3.IsEnabled = EvaluateEnabled(enabledField3.Text);
            }
        }

        private bool EvaluateValidation(string expression)
        {
            bool result = true;
            try
            {
                result = _evaluator.Evaluate(expression);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        private bool EvaluateEnabled(string expression)
        {
            bool result = true;

            try
            {
                result = _evaluator.Evaluate(expression);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var script = Script.Text;
            if (!string.IsNullOrWhiteSpace(script)) {
                Result.Text = "";
                //var result = _scriptEvaluator.ScriptEvaluate(script);
                var stringResult = _scriptEvaluator.ScriptEvaluate(script);
                stringResult = stringResult.Trim('\"');
                Result.Text = Regex.Unescape(stringResult);
            }
        }
    }
}
