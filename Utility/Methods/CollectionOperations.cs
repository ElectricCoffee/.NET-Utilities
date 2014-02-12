using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Methods
{
    public static class CollectionOperations
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> input, int chunksize)
        {
            var lst = new List<T>();
            var list = input.ToList();

            for (var i = 0; i < list.Count; i++)
            {
                lst.Add(list[i]);

                if (lst.Count == chunksize || i + 1 == list.Count)
                {
                    yield return lst;
                    lst = new List<T>();
                }
            }
        }
    }
}
