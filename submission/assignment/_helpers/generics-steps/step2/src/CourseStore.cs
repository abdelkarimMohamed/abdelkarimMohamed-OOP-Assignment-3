namespace GenericsLab;

public class CourseStore
{
    private readonly List<Course> _items = new();

    public void Add(Course course) => _items.Add(course);

    public Course? GetById(int id)
    {
        foreach (var course in _items)
        {
            if (course.Id == id)
                return course;
        }
        return null;
    }

    public List<Course> GetAll() => _items;

    public bool Remove(int id)
    {
        var course = GetById(id);
        return course is not null && _items.Remove(course);
    }
}
