namespace GenericsLab;

public static class EnumerableExtensions
{
    // Not an iterator itself, so the checks run immediately when Page() is called
    // (in an iterator they would only run when the caller starts looping).
    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "Page number must be 1 or greater.");
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Page size must be 1 or greater.");

        return PageIterator(source, pageNumber, pageSize);
    }

    private static IEnumerable<T> PageIterator<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var skip = (long)(pageNumber - 1) * pageSize;
        var index = 0L;

        foreach (var item in source)
        {
            if (index >= skip + pageSize)
                yield break;          // page is complete — stop reading the source

            if (index >= skip)
                yield return item;

            index++;
        }
    }

    public static T? FindById<T>(this IEnumerable<T> source, int id) where T : IHasId
    {
        ArgumentNullException.ThrowIfNull(source);

        foreach (var item in source)
        {
            if (item.Id == id)
                return item;
        }
        return default;
    }

    public static IReadOnlyDictionary<int, T> ToIdDictionary<T>(this IEnumerable<T> source) where T : IHasId
    {
        ArgumentNullException.ThrowIfNull(source);

        var result = new Dictionary<int, T>();
        foreach (var item in source)
        {
            if (!result.TryAdd(item.Id, item))
                throw new InvalidOperationException($"Duplicate Id {item.Id} in source.");
        }
        return result;
    }
}
