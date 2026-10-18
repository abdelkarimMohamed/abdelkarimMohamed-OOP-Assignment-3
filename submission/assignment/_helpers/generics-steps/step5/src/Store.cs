namespace GenericsLab;

public class Store<T> where T : IHasId
{
    // Keyed by Id: GetById is a direct lookup (O(1)), no loop.
    private readonly Dictionary<int, T> _items = new();

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (!_items.TryAdd(item.Id, item))
            throw new InvalidOperationException(
                $"Cannot add {typeof(T).Name} with Id {item.Id}: an item with this Id already exists.");
    }

    public T? GetById(int id) =>
        _items.TryGetValue(id, out var item) ? item : default;

    // Read-only view: callers can loop and count, but there is no Add/Remove on this type.
    public IReadOnlyCollection<T> GetAll() => _items.Values;

    public bool Remove(int id) => _items.Remove(id);
}
