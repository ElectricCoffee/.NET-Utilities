using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Classes
{
    /// <summary>
    /// The type of option
    /// </summary>
    enum OptType { Some, None };

    /// <summary>
    /// Base class that holds the information about which type is used
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class Option<T>
    {
        private readonly OptType _tag;

        protected Option(OptType tag)
        {
            _tag = tag;
        }

        public OptType Tag { get { return _tag; } }

        /// <summary>
        /// Is used in if statements
        /// </summary>
        /// <returns></returns>
        public bool MatchNone()
        {
            return Tag == OptType.None;
        }

        /// <summary>
        /// is used in if statements
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool MatchSome(out T value)
        {
            if (Tag == OptType.Some) value = ((Some<T>)this).Value;
            else value = default(T);
            return Tag == OptType.Some;
        }
    }

    class None<T> : Option<T> 
    {
        /// <summary>
        /// Empty constructor; None shouldn't contain anything
        /// </summary>
        public None() : base(OptType.None) { }
    }

    class Some<T> : Option<T>
    {
        private readonly T _value;

        /// <summary>
        /// Some constructor; stores any given value
        /// </summary>
        /// <param name="value"></param>
        public Some(T value) : base(OptType.Some)
        {
            _value = value;
        }

        public T Value { get { return _value; } }
    }

    /// <summary>
    /// Utility class for easier access
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// Used when something has no value, used in place of null
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <returns></returns>
        public static Option<T> None<T>()
        {
            return new None<T>();
        }

        /// <summary>
        /// Used when something has a value, but could have no value
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="value">Any kind of value</param>
        /// <returns></returns>
        public static Option<T> Some<T>(T value)
        {
            return new Some<T>(value);
        }
    }
}
