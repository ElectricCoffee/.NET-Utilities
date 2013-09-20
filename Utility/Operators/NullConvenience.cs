using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Operators
{
    public static class NullConvenience
    {
        public static T GetOrElse<T>(this T obj, T na) where T : IEquatable<T>
        {
            if (EqualityComparer<T>.Default.Equals(obj, default(T)))
                return obj;
            else
                return na;
        }

        public static void SetIfDefault<T>(this T obj, T na) where T : IEquatable<T>
        {
            if (EqualityComparer<T>.Default.Equals(obj, default(T)))
                obj = na;
        }

        public static TOut NavigateSafely<TIn, TOut>(this TIn obj, Func<TIn, TOut> navigation) where TIn : IEquatable<TIn>
        {
            if (EqualityComparer<TIn>.Default.Equals(obj, default(TIn)))
                return default(TOut);
            else
                return navigation(obj);
        }
    }
}
