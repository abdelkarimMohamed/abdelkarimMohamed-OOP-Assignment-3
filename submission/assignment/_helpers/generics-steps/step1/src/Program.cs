using GenericsLab;

var students = new StudentStore();
students.Add(new Student { Id = 1, Name = "Ahmed" });
students.Add(new Student { Id = 2, Name = "Mona" });

Console.WriteLine(students.GetById(2));
Console.WriteLine(students.GetById(99)?.ToString() ?? "not found");
