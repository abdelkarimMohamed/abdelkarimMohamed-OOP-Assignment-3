namespace GenericsLab;

public class Course : IHasId
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public decimal Price { get; set; }

    public override string ToString() => $"Course #{Id}: {Title} ({Price} EGP)";
}
