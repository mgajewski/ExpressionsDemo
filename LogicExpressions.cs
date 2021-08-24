using CodingSeb.ExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionsDemo
{
    public class LogicExpressions
    {
        //private ExpressionEvaluator evaluator = new ExpressionEvaluator();
        private CustomizedEvaluator evaluator = new CustomizedEvaluator();

        public LogicExpressions(IDictionary<string, object> variables)
        {
            evaluator.Variables = variables;
        }

        public LogicExpressions(object context)
        {
            evaluator.Context = context;
        }

        public bool Evaluate(string expression)
        {
            return evaluator.Evaluate<bool>(expression);
        }
    }
}
