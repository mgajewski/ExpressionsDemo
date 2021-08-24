using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionsDemo
{
    public static class StringExtensions
    {
        public static string RemoveA(this string str)
        {
            return str.Replace("a", "");
        }
    }
}
