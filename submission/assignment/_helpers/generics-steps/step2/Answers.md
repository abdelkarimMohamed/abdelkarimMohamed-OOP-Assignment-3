# Generics — answers

---

## Step 2 — What is the same and what is different between the two stores?

**The same:** almost everything. Both keep their items in a `List<>`, and the four methods have exactly the same logic — `Add` appends, `GetById` loops and compares `Id`, `GetAll` returns the list, `Remove` finds by `Id` and removes. `CourseStore` was literally created by copying `StudentStore` and replacing the word `Student` with `Course`.

**Different:** only the **type of the item** (`Student` vs `Course`). The stores don't care about `Name`, `Title` or `Price` at all — the only property they use is `Id`.

This duplication is the problem: every new entity (Teacher, Room, ...) would need another copy, and a bug fix would have to be repeated in every copy. The type is the only thing that changes, which is exactly what a **type parameter** (generics) is for.
