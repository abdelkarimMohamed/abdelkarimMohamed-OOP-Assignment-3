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

---

## Step 7 — Why must `new Store<string>()` NOT compile?

`Store<T>` is declared with the constraint `where T : IHasId`. That constraint is a rule checked by the compiler: *only types that implement `IHasId` may be used as `T`*. `string` does not implement `IHasId` (it has no `Id` at all), so the compiler rejects it with error **CS0311** ("The type 'string' cannot be used as type parameter 'T' ... There is no implicit reference conversion from 'string' to 'IHasId'").

This is the whole value of the constraint: `GetById` relies on `item.Id`, so if `Store<string>` were allowed, that code could not work. The mistake is caught at **compile time**, before the program ever runs, instead of becoming a crash at runtime.

---

## Last question — what is the common name for the class built in Steps 4 and 5?

A **Generic Repository** (the *Repository* pattern implemented with generics).
It is one reusable, type-safe class (`Store<T> where T : IHasId`) that provides the standard data-access operations — add, get by id, get all, remove — for any entity type, so each entity doesn't need its own store class. In Step 5 it also behaves like an **in-memory repository**, because it keeps the data in a `Dictionary` instead of a database.
