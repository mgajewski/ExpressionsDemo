using CodingSeb.ExpressionEvaluator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionsDemo
{
    public class ScriptEvaluator
    {
        private ExpressionEvaluator evaluator = new ExpressionEvaluator();
        //private ExpressionEvaluator evaluator = new CustomizedEvaluator();

        public ScriptEvaluator()
        {
            // Extension methods
            
            //evaluator.StaticTypesForExtensionsMethods.Add(typeof(StringExtensions));

            // Script:
            // text = "ala ma kota";
            // text.RemoveA();

            //------------------------------------

            // Context object
            
            //evaluator.Context = new ScriptsContext()
            //{
            //    field = "Rambutan",
            //    Property = "Hello, World"
            //};
            
            // Scripts:
            // field;
            // Property;
            // AgePlusX(10);

            // -----------------------------------

            // Add types:
            //evaluator.Types.Add(typeof(ScriptsContext.InternalClass));
            // Script:
            // a = new InternalClass(2, 3);
            // a.C;
        }

        public string ScriptEvaluate(string script)
        {
            try
            {
                ClearVariables();
                var result = evaluator.ScriptEvaluate(script);
                string serializedResult = JsonConvert.SerializeObject(result, Formatting.Indented);
                return serializedResult;
            }
            catch(Exception exception)
            {
                return $"{exception.GetType().Name}: {exception.Message}";
            }
        }

        private void ClearVariables()
        {
            evaluator.Variables.Clear();
        }

    }
}
