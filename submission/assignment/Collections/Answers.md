# Collections — answers

---

## Task 2.1 — IReadOnlyDictionary & SortedDictionary

### What is each one?

- **`IReadOnlyDictionary<TKey, TValue>`** is an **interface**, not a collection. It describes a key/value collection that can only be *read*: it has the indexer (get only), `ContainsKey`, `TryGetValue`, `Keys`, `Values`, `Count` and enumeration — and no `Add`, `Remove` or indexer setter. `Dictionary<TKey, TValue>` implements it, so any dictionary can be handed out "as" read-only.
- **`SortedDictionary<TKey, TValue>`** is a real **collection class**. It stores key/value pairs **always sorted by key**. Internally it is a balanced binary search tree (a red-black tree), not a hash table.

### How is IReadOnlyDictionary different from Dictionary, and why would a public method return one?

`Dictionary` is a concrete class you can read **and change**. `IReadOnlyDictionary` is only a *view* that exposes the read operations. If a public method returns a `Dictionary`, every caller can add, remove or overwrite entries and silently break the owner's data. Returning `IReadOnlyDictionary` makes the **contract** clear — "you may look, not touch" — and the compiler stops callers from calling `Add` or the setter. It also hides the implementation, so the owner can later switch to another type without breaking callers.

Note: it is a read-only *view*, not a copy and not immutable. If the owner changes the underlying dictionary, the caller sees the change. (For a truly unchangeable copy there are `ReadOnlyDictionary` wrappers, `FrozenDictionary` and `ImmutableDictionary`.)

### How is SortedDictionary different from Dictionary in ordering and lookup speed?

| | `Dictionary` | `SortedDictionary` |
|---|---|---|
| Internal structure | Hash table | Red-black tree |
| Order when you loop | Not guaranteed — don't rely on it | Always sorted by key |
| Lookup / add / remove | **O(1)** on average | **O(log n)** |
| Key requirement | Good `GetHashCode` + `Equals` | Keys must be comparable (`IComparer<TKey>`) |

So `Dictionary` is faster for pure lookups, and `SortedDictionary` pays a little speed to keep the keys in order at all times.

### When would you choose SortedDictionary?

When you need the data **in key order often** while it keeps changing — items are added and removed at any time and you must iterate in sorted order (timetables by time, leaderboards by score, logs by date). Sorting a normal `Dictionary` every time you print it would be wasteful. If the data is filled once and then only read, a `Dictionary` (sorted once when needed) or a `SortedList` (less memory, but slower inserts) can be better.

### Sources

1. Microsoft Learn — `IReadOnlyDictionary<TKey,TValue>` Interface: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2
2. Microsoft Learn — `SortedDictionary<TKey,TValue>` Class: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sorteddictionary-2
3. Microsoft Learn — Sorted collection types: https://learn.microsoft.com/en-us/dotnet/standard/collections/sorted-collection-types
4. _(add the second, non-Microsoft source you read here)_

---

## Task 2.2 — Pick the Collection

| # | Scenario | Collection | Why |
|---|---|---|---|
| S1 | Find a student by national ID, thousands of times a day | `Dictionary<string, Student>` | Lookup by key is O(1) on average, so frequent searches stay fast no matter how many students there are. |
| S2 | Course tags, the same tag never stored twice | `HashSet<string>` | A set rejects duplicates by design and checks membership in O(1). |
| S3 | Grades in the order entered, duplicates allowed | `List<int>` | A list keeps insertion order and allows the same value many times. |
| S4 | Public method returns the price list, read-only for callers | `IReadOnlyDictionary<string, decimal>` | Callers can look up prices by course, but the type has no `Add` or setter, so they cannot change the list. |
| S5 | Timetable keyed by start time, added any time, always printed in time order | `SortedDictionary<DateTime, Session>` | It keeps keys sorted on every insert, so it always prints in time order without re-sorting. |
| S6 | Results the caller loops over once and may stop early | `IEnumerable<T>` | It can be produced lazily with `yield return`, so items are only created as the caller asks — stopping early skips the rest. |
