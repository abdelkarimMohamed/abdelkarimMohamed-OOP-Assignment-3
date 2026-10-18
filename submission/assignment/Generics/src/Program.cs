using GenericsLab;

// ---------- 5 students + 3 courses ----------
var students = new Store<Student>();
students.Add(new Student { Id = 1, Name = "Ahmed" });
students.Add(new Student { Id = 2, Name = "Mona" });
students.Add(new Student { Id = 3, Name = "Omar" });
students.Add(new Student { Id = 4, Name = "Sara" });
students.Add(new Student { Id = 5, Name = "Youssef" });

var courses = new Store<Course>();
courses.Add(new Course { Id = 10, Title = "C# Basics", Price = 1500m });
courses.Add(new Course { Id = 20, Title = "ASP.NET Core", Price = 2500m });
courses.Add(new Course { Id = 30, Title = "Entity Framework", Price = 2000m });

Console.WriteLine("=== GetById ===");
Console.WriteLine(students.GetById(3));
Console.WriteLine(courses.GetById(20));

// ---------- duplicate id ----------
Console.WriteLine();
Console.WriteLine("=== Duplicate Id ===");
try
{
    students.Add(new Student { Id = 3, Name = "Another Omar" });
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// ---------- page 2, size 2 ----------
Console.WriteLine();
Console.WriteLine("=== Students page 2 (size 2) ===");
foreach (var student in students.GetAll().Page(2, 2))
    Console.WriteLine(student);

// ---------- FindById on a plain List<Course> ----------
Console.WriteLine();
Console.WriteLine("=== FindById on List<Course> ===");
var courseList = new List<Course>
{
    new() { Id = 100, Title = "Clean Code", Price = 900m },
    new() { Id = 200, Title = "Design Patterns", Price = 1200m },
};
Console.WriteLine(courseList.FindById(200));
Console.WriteLine(courseList.FindById(999)?.ToString() ?? "Id 999 not found (default)");

// ---------- ToIdDictionary ----------
Console.WriteLine();
Console.WriteLine("=== ToIdDictionary ===");
var byId = courses.GetAll().ToIdDictionary();
Console.WriteLine($"Dictionary has {byId.Count} courses; byId[30] = {byId[30]}");

// ---------- must NOT compile ----------
// var strings = new Store<string>();   // must NOT compile: string does not implement IHasId
