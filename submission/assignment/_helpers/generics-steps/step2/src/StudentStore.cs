namespace GenericsLab;

public class StudentStore
{
    private readonly List<Student> _items = new();

    public void Add(Student student) => _items.Add(student);

    public Student? GetById(int id)
    {
        foreach (var student in _items)
        {
            if (student.Id == id)
                return student;
        }
        return null;
    }

    public List<Student> GetAll() => _items;

    public bool Remove(int id)
    {
        var student = GetById(id);
        return student is not null && _items.Remove(student);
    }
}
