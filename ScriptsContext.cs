using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionsDemo
{
    public class ScriptsContext
    {
        public string field;
        public string Property { get; set; }
        private int _age = 20;

        public int AgePlusX(int x)
        {
            return _age + x;
        }

        public class InternalClass
        {
            public int C { get; }
            public InternalClass(int a, int b)
            {
                C = a + b;
            }
        }
    }
}
