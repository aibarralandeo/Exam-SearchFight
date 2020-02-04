using System;
using System.Collections.Generic;

namespace SearchFight.Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static T MaxValue<T>(this IEnumerable<T> source, Func<T, long> func)
        {
            if (source != null)
                using (var en = source.GetEnumerator())
                {
                    if (!en.MoveNext()) throw new ArgumentException();
                    var max = func(en.Current);
                    var maxValue = en.Current;

                    while (en.MoveNext())
                    {
                        var possible = func(en.Current);

                        if (max >= possible)
                            continue;

                        max = possible;
                        maxValue = en.Current;
                    }

                    return maxValue;
                }

            throw new ArgumentException();
        }
    }
}
