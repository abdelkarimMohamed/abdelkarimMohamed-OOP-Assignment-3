# Generics — answers

---

## Step 2 — What is the same and what is different between the two stores?

**The same:** almost everything. Both keep their items in a `List<>`, and the four methods have exactly the same logic — `Add` appends, `GetById` loops and compares `Id`, `GetAll` returns the list, `Remove` finds by `Id` and removes. `CourseStore` was literally created by copying `StudentStore` and replacing the word `Student` with `Course`.

**Different:** only the **type of the item** (`Student` vs `Course`). The stores don't care about `Name`, `Title` or `Price` at all — the only property they use is `Id`.

This duplication is the problem: every new entity (Teacher, Room, ...) would need another copy, and a bug fix would have to be repeated in every copy. The type is the only thing that changes, which is exactly what a **type parameter** (generics) is for.

---

## Step 3 — Why doesn't `GetById` compile?

Compiler error:

```
Store.cs(14,22): error CS1061: 'T' does not contain a definition for 'Id' and no accessible extension method 'Id' accepting a first argument of type 'T' could be found (are you missing a using directive or an assembly reference?)
```

**Why:** inside `Store<T>` the compiler has to check the code **once**, for *every* type that could ever be used as `T` — not only for `Student` and `Course`. Without any constraint, `T` could be `int`, `string`, `DateTime` or anything else, and most of those types have no `Id` property. So the only members the compiler allows on `T` are the ones every type has (the members of `object`: `ToString`, `Equals`, `GetHashCode`...). The fact that `Student` and `Course` both happen to have an `Id` is a coincidence the compiler cannot see — they share a property *name*, not a type. To use `item.Id`, we must promise the compiler that every `T` will have an `Id` (Step 4).
