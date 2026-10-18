using System.Diagnostics;
using LinkedInDemos;

// =========================================================
// 1) HashSet.Contains vs List.Contains
// =========================================================
Console.WriteLine("=== 1) List.Contains vs HashSet.Contains ===");
const int size = 100_000;
const int lookups = 10_000;

var list = new List<int>();
for (var i = 0; i < size; i++) list.Add(i);
var set = new HashSet<int>(list);

var rnd = new Random(1);
var targets = new int[lookups];
for (var i = 0; i < lookups; i++) targets[i] = rnd.Next(size * 2); // ~half are misses (worst case for List)

var sw = Stopwatch.StartNew();
var hitsList = 0;
foreach (var t in targets) if (list.Contains(t)) hitsList++;
sw.Stop();
Console.WriteLine($"List.Contains    → {sw.ElapsedMilliseconds,5} ms (hits={hitsList})");

sw.Restart();
var hitsSet = 0;
foreach (var t in targets) if (set.Contains(t)) hitsSet++;
sw.Stop();
Console.WriteLine($"HashSet.Contains → {sw.ElapsedMilliseconds,5} ms (hits={hitsSet})");

// =========================================================
// 2) yield
// =========================================================
Console.WriteLine();
Console.WriteLine("=== 2) yield ===");
foreach (var n in YieldDemo.Numbers())
{
    if (n > 3) break;
    Console.WriteLine($"  got {n}");
}

// =========================================================
// 3) Span<T>
// =========================================================
Console.WriteLine();
Console.WriteLine("=== 3) Span<T> ===");
SpanDemo.Run();

// =========================================================
// 4) Extension members (C# 14 / .NET 10)
// =========================================================
Console.WriteLine();
Console.WriteLine("=== 4) Extension members ===");
ExtensionMembersDemo.Run();
