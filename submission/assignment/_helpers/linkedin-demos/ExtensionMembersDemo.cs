namespace LinkedInDemos;

// BEFORE — classic extension method (C# 3+): methods only, "this" on each one
public static class ClassicStringExtensions
{
    public static bool IsBlank(this string? s) => string.IsNullOrWhiteSpace(s);
}

// AFTER — C# 14 extension block: properties + static members
public static class ModernStringExtensions
{
    extension(string? s)                 // instance members: "receiver" is s
    {
        public bool IsEmptyOrSpaces => string.IsNullOrWhiteSpace(s);   // extension PROPERTY
        public int WordCount =>
            s is null ? 0 : s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    extension(string)                    // static members: called on the type itself
    {
        public static string Repeat(string text, int times) => string.Concat(Enumerable.Repeat(text, times));
    }
}

public static class ExtensionMembersDemo
{
    public static void Run()
    {
        string? name = "  ";
        Console.WriteLine($"  classic method : {name.IsBlank()}");
        Console.WriteLine($"  new property   : {name.IsEmptyOrSpaces}");
        Console.WriteLine($"  word count     : {"Hello from C# 14".WordCount}");
        Console.WriteLine($"  static member  : {string.Repeat("ab", 3)}");
    }
}
