#!/usr/bin/env bash
# Creates the whole commit history for OOP Assignment 3.
# Run ONCE from the repository root, on branch assignment/1-7:
#     bash _helpers/commit-all.sh
set -euo pipefail

BRANCH="assignment/1-7"
H="_helpers"

[ -d .git ] || { echo "Run this from the repository root."; exit 1; }
[ -d "$H" ]  || { echo "_helpers folder not found."; exit 1; }
current=$(git rev-parse --abbrev-ref HEAD)
if [ "$current" != "$BRANCH" ]; then
  echo "You are on '$current'. First run: git checkout -b $BRANCH"
  exit 1
fi

# keep _helpers out of git (local only, not committed)
grep -qxF "$H/" .git/info/exclude 2>/dev/null || echo "$H/" >> .git/info/exclude

commit() {   # commit "<message>" <paths...>
  local msg="$1"; shift
  git add -A -- "$@"
  if git diff --cached --quiet; then echo "  (nothing to commit: $msg)"; else git commit -q -m "$msg"; echo "✔ $msg"; fi
}

TMP=$(mktemp -d)
cp -r Refactoring "$TMP/Refactoring"

# 1) README + .gitignore
commit "Add README and .gitignore" README.md .gitignore

# 2) original lab, untouched — so the PR diff shows every refactoring
rm -rf Refactoring && mkdir -p Refactoring
cp -r "$H/original-lab/part-01" "$H/original-lab/part-02" "$H/original-lab/part-03" Refactoring/
commit "Copy original Refactoring lab (part-01, part-02, part-03)" Refactoring

# 3) refactored parts, one commit each
for p in part-01 part-02 part-03; do
  rm -rf "Refactoring/$p" && cp -r "$TMP/Refactoring/$p" "Refactoring/$p"
done
commit "Part 01: OCP carriers, DIP OrderProcessor, Decorator notifications + new carrier and channel" Refactoring/part-01
commit "Part 02: Template Method for report exporters, Facade for enrollment" Refactoring/part-02
commit "Part 03: HashSet lookup for blocked users, lazy yield StudentCatalog" Refactoring/part-03

# 4) Collections
commit "Collections: research answers (2.1, 2.2) and regex extension methods (2.3)" Collections

# 5) Generics — one commit per step
msgs=(
  ""
  "Step 1: StudentStore with List<Student>"
  "Step 2: CourseStore + compare the two stores"
  "Step 3: generic Store<T> (GetById does not compile - expected)"
  "Step 4: IHasId constraint, remove StudentStore and CourseStore"
  "Step 5: Dictionary storage, duplicate check, read-only GetAll"
  "Step 6: generic extension methods Page, FindById, ToIdDictionary"
  "Step 7: prove it in Main + Store<string> must not compile"
)
for i in 1 2 3 4 5 6 7; do
  rm -rf Generics && cp -r "$H/generics-steps/step$i" Generics
  commit "${msgs[$i]}" Generics
done

# 6) LinkedIn
commit "LinkedIn: add post links" LinkedIn

rm -rf "$TMP"
echo
echo "Done. Check with:  git log --oneline"
echo "Then put your 4 real links in LinkedIn/linked.md, commit, and push:"
echo "  git push -u origin $BRANCH"
