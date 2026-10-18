namespace LinkedInDemos;

public static class SpanDemo
{
    public static void Run()
    {
        // Strings: slice "2026-10-16" without creating new strings
        ReadOnlySpan<char> date = "2026-10-16";
        var year  = int.Parse(date[..4]);
        var month = int.Parse(date.Slice(5, 2));
        var day   = int.Parse(date[8..]);
        Console.WriteLine($"  year={year}, month={month}, day={day}");

        // Arrays: a span is a window over the SAME memory
        int[] numbers = { 1, 2, 3, 4, 5, 6 };
        Span<int> middle = numbers.AsSpan(2, 3); // 3, 4, 5 — no copy
        middle[0] = 99;                          // changes the original array
        Console.WriteLine($"  numbers = [{string.Join(", ", numbers)}]");
    }
}
