# Part 03 — answers

> Measured with `dotnet run` (Debug) — 50,000 blocked ids, 5,000 requests, average of 3 runs.

---

## BlockedUsers

- **Time complexity before:** `O(n × m)` — for each of the *m* request ids, `List<int>.Contains` does a linear scan over the *n* blocked ids. With n = 50,000 and m = 5,000 that is up to 250,000,000 comparisons.
- **Time (ms) before:** ~24 ms (`found=5000`)
- **What did you change?**
  `CountBlocked` now copies the blocked ids into a `HashSet<int>` once, then checks each request with `HashSet.Contains`. A hash set finds an item by its hash code instead of scanning, so each lookup is O(1) on average. The method signature, inputs and the `found` count are unchanged.
- **Time complexity after:** `O(n + m)` — O(n) to build the set once, then O(1) per lookup for the *m* requests.
- **Time (ms) after:** ~6 ms (`found=5000` — unchanged)

---

## Students

- **What was the problem?**
  `GetAllStudents` was **eager**: it built a `List<Student>` with all 1,000,000 students (1,000,000 objects + 1,000,000 strings) before returning anything. `Program.cs` only prints the first 3 and then `break`s, so 999,997 students were created for nothing — wasted time and memory.

- **What did you change?**
  I made it **lazy** with `yield return` and changed the return type to `IEnumerable<Student>`. Now the method is an iterator: each student is created only when the `foreach` asks for the next item. When `Program.cs` breaks after 3 students, the loop inside `GetAllStudents` stops too — only 3 students are ever created. `Program.cs` did not need to change, and it still prints the same first 3 students.
