namespace LinkedInDemos;

public static class YieldDemo
{
    public static IEnumerable<int> Numbers()
    {
        for (var i = 1; i <= 1_000_000; i++)
        {
            Console.WriteLine($"  producing {i}");
            yield return i;           // hand one item back, pause here
            if (i == 10) yield break; // stop the sequence completely
        }
    }
}
