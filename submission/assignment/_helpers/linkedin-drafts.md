# LinkedIn post drafts — rewrite these in your own words before posting

> Run `_helpers/linkedin-demos` (`dotnet run -c Release`) and put **your own** numbers in post 1.
> Post visibility: **Anyone**. After posting, paste the 4 links into `LinkedIn/linked.md`.

---

## 1) HashSet.Contains vs List.Contains

I measured something that surprised me 👇

Same data (100,000 ints), same 10,000 lookups:
- `List.Contains` → **___ ms**
- `HashSet.Contains` → **___ ms**

Why?
🔹 `List.Contains` checks items one by one → **O(n)**. 10,000 lookups × up to 100,000 items.
🔹 `HashSet.Contains` computes the item's hash and jumps straight to its bucket → **O(1)** on average.

```csharp
var list = new List<int>(); for (var i = 0; i < 100_000; i++) list.Add(i);
var set  = new HashSet<int>(list);

var sw = Stopwatch.StartNew();
foreach (var t in targets) list.Contains(t);
Console.WriteLine($"List: {sw.ElapsedMilliseconds} ms");

sw.Restart();
foreach (var t in targets) set.Contains(t);
Console.WriteLine($"HashSet: {sw.ElapsedMilliseconds} ms");
```

Rule of thumb: if the main question you ask a collection is "is X in here?", use a `HashSet`.
Trade-off: no order, no duplicates, a bit more memory.

#dotnet #csharp #performance

---

## 2) The `yield` keyword

`yield` lets a method return items **one at a time, only when asked**.

```csharp
IEnumerable<int> Numbers()
{
    for (var i = 1; i <= 1_000_000; i++)
    {
        Console.WriteLine($"producing {i}");
        yield return i;            // give one item, pause here
        if (i == 10) yield break;  // end the sequence
    }
}

foreach (var n in Numbers())
{
    if (n > 3) break;
}
// prints "producing" only 4 times — not 1,000,000
```

🔹 `yield return` → hands back one item and pauses the method.
🔹 `yield break` → ends the sequence.
🔹 Building a `List` first creates **everything** even if the caller only needs 3 items. `yield` is **lazy**: work happens only when the caller loops.

⚠️ Watch out: the method body doesn't run when you *call* it — only when you start looping. So argument checks inside an iterator are delayed. Validate in a normal wrapper method, then call a private iterator. Also: looping twice runs the whole thing twice.

#dotnet #csharp

---

## 3) Span<T>

`Span<T>` is a **window over existing memory** — part of an array, a string, or the stack — with **no copy**.

```csharp
ReadOnlySpan<char> date = "2026-10-16";
int year  = int.Parse(date[..4]);        // no new string
int month = int.Parse(date.Slice(5, 2)); // no new string

int[] numbers = { 1, 2, 3, 4, 5, 6 };
Span<int> middle = numbers.AsSpan(2, 3); // 3, 4, 5
middle[0] = 99;                          // numbers is now 1, 2, 99, 4, 5, 6
```

`Substring` would allocate 3 new strings. With a span → zero allocations. Less garbage, less GC work.

⚠️ Limitation: `Span<T>` is a `ref struct` — it lives on the stack only. You can't store it in a class field, box it, use it inside `async` methods across an `await`, or capture it in a lambda. Need that? Use `Memory<T>`.

📚 Read: Microsoft Learn — "Memory and span-related types"

#dotnet #csharp #performance

---

## 4) Extension members in .NET 10 (C# 14)

Extension methods have existed since C# 3. C# 14 adds **extension members**: extension *properties* and *static* extension members.

**Before** — methods only:
```csharp
public static class StringExtensions
{
    public static bool IsBlank(this string? s) => string.IsNullOrWhiteSpace(s);
}
name.IsBlank();
```

**After** — an `extension` block:
```csharp
public static class StringExtensions
{
    extension(string? s)
    {
        public bool IsEmptyOrSpaces => string.IsNullOrWhiteSpace(s); // property!
        public int WordCount => s?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length ?? 0;
    }

    extension(string)   // no receiver name → static members on the type
    {
        public static string Repeat(string text, int times) => string.Concat(Enumerable.Repeat(text, times));
    }
}

name.IsEmptyOrSpaces;        // property, no ()
"Hello from C# 14".WordCount;
string.Repeat("ab", 3);      // looks like it's built into string
```

What it adds:
🔹 Extension **properties**
🔹 **Static** extension members, called on the type itself
🔹 One receiver declared once for a whole group of members

Old `this` extension methods still work, and both styles can live side by side.

📚 Read: Microsoft Learn — "Extension members" (C# 14)

#dotnet #csharp #dotnet10
