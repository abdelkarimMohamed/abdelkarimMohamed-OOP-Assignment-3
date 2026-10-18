namespace GenericsLab;

// The constraint tells the compiler every T has an Id.
public class Store<T> where T : IHasId
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);

    public T? GetById(int id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id)
                return item;
        }
        return default;
    }

    public List<T> GetAll() => _items;

    public bool Remove(int id)
    {
        var item = GetById(id);
        return item is not null && _items.Remove(item);
    }
}
