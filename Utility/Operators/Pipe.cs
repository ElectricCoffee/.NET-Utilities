using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Operators
{
    public static class Pipe
    {
        public static T PipeTo<T>(this T obj, Action<T> act) // "hello".PipeTo(s => MethodUsingAString(s))
        {
            act(obj);
            return obj;
        }

        public static T2 PipeTo<T1, T2>(this T1 obj, Func<T1, T2> func) // "hello".PipeTo(s => int.Parse(s))
        {
            return func(obj);
        }

        public static T BackPipeTo<T>(this Action<T> act, T obj)
        {
            act(obj);
            return obj;
        }

        public static T2 BackPipeTo<T1, T2>(this Func<T1, T2> func, T1 obj)
        {
            return func(obj);
        }
    }
}
