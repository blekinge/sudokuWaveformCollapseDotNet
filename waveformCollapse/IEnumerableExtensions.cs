namespace waveformCollapse;

using System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        => self.Select((item, index) => (item, index));

    public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        => self.ToList()
               .ForEach(action);
}