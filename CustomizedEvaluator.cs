using CodingSeb.ExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ExpressionsDemo
{
    //Wymagania:
    //1. Wyrażenia istnieją w bazie po stronie klienta - działały w starej aplikacji
    //2. Implementacja interpretera powinna poradzić sobie z tymi wyrażeniami
    //3. Operatory:
    //    1. "=" - porównanie
    //    2. "&&" => "AND"
    //    3. "||" => "OR"
    //    3. "LIKE" - prównanie z wildcardem / Regex (*.+)
    //4. Ze względów bezpieczeństwa należy zablokować możliwość wykonania złośliwego kodu:
    //    1. Brak możliwości deklarowania i przypisywania zmiennych
    //    2. Brak operatora new
    //    3. Dostęp tylko do wybranych typów

    public class CustomizedEvaluator : ExpressionEvaluator
    {
        // 4. Definiujemy nowy operator
        protected new static readonly IList<IDictionary<ExpressionOperator, Func<dynamic, dynamic, object>>> operatorsEvaluations =
        ExpressionEvaluator.operatorsEvaluations
            .Copy()
            .AddOperatorEvaluationAtLevelOf(CustomizedExpressionOperator.Like, LikeFunction, ExpressionOperator.Equal);

        protected override IList<IDictionary<ExpressionOperator, Func<dynamic, dynamic, object>>> OperatorsEvaluations => operatorsEvaluations;

        protected override void Init()
        {
            // 1. zmieniamy definicje operatora "="
            operatorsDictionary.Remove("=");
            operatorsDictionary.Add("=", ExpressionOperator.Equal);

            // 2. Dodajemy nowe literały dla istniejących operatorów
            operatorsDictionary.Add("AND", ExpressionOperator.ConditionalAnd);
            operatorsDictionary.Add("OR", ExpressionOperator.ConditionalOr);
            
            // 3. Wyrzucamy niepotrzebne operatory
            operatorsDictionary.Remove("&&");
            operatorsDictionary.Remove("||");
            operatorsDictionary.Remove("==");

            // 4. Definiujemy nowy operator
            operatorsDictionary.Add("LIKE", CustomizedExpressionOperator.Like);

            // 5. Ustawiamy opcje
            SetupOptions();
        }

        private static object LikeFunction(dynamic left, dynamic right)
        {
            return Regex.IsMatch(left, right);
        }

        private void SetupOptions()
        {
            //Defaults:
            /*
            OptionCaseSensitiveEvaluationActive = true;
            OptionForceIntegerNumbersEvaluationsAsDoubleByDefault = false;
            CultureInfoForNumberParsing = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            OptionNumberParsingDecimalSeparator = ".";
            OptionNumberParsingThousandSeparator = string.Empty;
            OptionFunctionArgumentsSeparator = ",";
            OptionInitializersSeparator = ",";
            OptionInlineNamespacesEvaluationActive = true;  //doc = new System.Xml.XmlDocument();
            OptionFluidPrefixingActive = true;              //List("hello", "bye").FluidAdd("test").Count
            OptionNewFunctionEvaluationActive = true;       //new(typeof(MyObject))
            OptionNewKeywordEvaluationActive = true;        //new MyObject
            OptionStaticMethodsCallActive = true;           //string.Format("Hello {0}", name)
            OptionStaticPropertiesGetActive = true;         //string.Empty
            OptionInstanceMethodsCallActive = true;         //myvar.ToString()
            OptionInstancePropertiesGetActive = true;       //myStringVar.Length
            OptionIndexingActive = true;                    //myArray[2]
            OptionStringEvaluationActive = true;            //$"Hello {name}"
            OptionCharEvaluationActive = true;              //'a' - single quotes evaluates string
            OptionEvaluateFunctionActive = true;            //Evaluate("1+2")
            OptionVariableAssignationActive = true;         //a = 1
            OptionPropertyOrFieldSetActive = true;          //a.Prop = 1
            OptionIndexingAssignationActive = true;         //myArray[2] = "Hello"
            OptionScriptEvaluateFunctionActive = true;      //ScriptEvaluate("value = 1+2;\r\nif(value > 2)\r\nreturn \"OK\";\r\nelse\r\nreturn \"NOK\";")
            OptionOnNoReturnKeywordFoundInScriptAction = OptionOnNoReturnKeywordFoundInScriptAction.ReturnAutomaticallyLastEvaluatedExpression;
            OptionScriptNeedSemicolonAtTheEndOfLastExpression = true;
            OptionAllowNonPublicMembersAccess = false;
            */

            //Requiered setup:
            OptionInlineNamespacesEvaluationActive = false;
            OptionFluidPrefixingActive = false;
            OptionNewFunctionEvaluationActive = false;
            OptionNewKeywordEvaluationActive = false;
            OptionEvaluateFunctionActive = false;
            OptionScriptEvaluateFunctionActive = false;
            OptionVariableAssignationActive = false;
            OptionPropertyOrFieldSetActive = false;
            OptionIndexingAssignationActive = false;

            Namespaces.Clear();     //removes everything: https://github.com/codingseb/ExpressionEvaluator/wiki/C%23-Types-Management#assemblies-namespaces-and-types
            TypesToBlock.Add(typeof(ExpressionEvaluator));
            TypesToBlock.Add(typeof(CustomizedEvaluator));
        }
    }

    // 4. Definiujemy nowy operator
    public class CustomizedExpressionOperator : ExpressionOperator
    {
        public static readonly ExpressionOperator Like = new CustomizedExpressionOperator();
    }
}
