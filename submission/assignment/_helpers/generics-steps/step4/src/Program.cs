using GenericsLab;

var students = new Store<Student>();
students.Add(new Student { Id = 1, Name = "Ahmed" });
students.Add(new Student { Id = 2, Name = "Mona" });
Console.WriteLine(students.GetById(2));

var courses = new Store<Course>();
courses.Add(new Course { Id = 10, Title = "C# Basics", Price = 1500m });
courses.Add(new Course { Id = 20, Title = "ASP.NET Core", Price = 2500m });
Console.WriteLine(courses.GetById(20));
