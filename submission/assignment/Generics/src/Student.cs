namespace GenericsLab;

public class Student : IHasId
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public override string ToString() => $"Student #{Id}: {Name}";
}
