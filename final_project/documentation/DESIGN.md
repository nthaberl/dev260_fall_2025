# Project Design & Rationale
# Incomplete
**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each)._

**Your Answer:**

**Entity A:**

- Name: Recipe
- Key fields: Name (string), Ingredients (List<String>)
- Identifiers: Name (case-insensitive)
- Relationships: A Recipe contains multiple ingredient "groups", Recipes are evaluated against the campfire to determine cookability 

**Entity B (if applicable):**

- Name: 
- Key fields:
- Identifiers:
- Relationships:

**Identifiers (keys) and why they're chosen:**  
_Explain your choice of keys (e.g., string Id, composite key, case-insensitive, etc.)._

**Your Answer:**  
Recipe names are used as identifiers because they are human-readable, unique within the dataset, and meaningful to the user.

---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
_Name the data structure (e.g., Dictionary<string, Customer>)._

**Your Answer:**  

List<Recipe>

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**  
Stores all recipes loaded from the JSON file and supports listing, searching, and iteration when checking cookable recipes.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

**Alternatives considered:**  
_List alternatives (e.g., List<T>, SortedDictionary, custom tree) and why you didn't choose them._

**Your Answer:**

---

### Structure #2

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**  
HashSet<string>

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**  
Used to store:
- All unique ingredients loaded from JSON
- Current campfire ingredients

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**  
- Guarantees uniqueness (no duplicate ingredients)
- Fast lookup and insertion: O(1) average
- Case-insensitive comparer prevents duplicate entries caused by casing
- Ideal for membership checks (Contains) during cooking logic

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**

---

### Structure #3

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**  
Dictionary<string, bool>

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**  
Stores the user’s recipe book, mapping recipe names to a cooked/not cooked status.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**  
- Fast access by recipe name: O(1) average
- Enforces uniqueness of saved recipes
- Boolean value efficiently represents cooked status
- Case-insensitive keys prevent duplicate saved recipes

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**
List<Recipe>: would require linear searches and duplicate checks
Custom object structure: unnecessary complexity for current scope
---

### Additional Structures (if applicable)

_Add more sections if you used additional structures like Queue for workflows, Stack for undo, HashSet for uniqueness, Graph for relationships, BST/SortedDictionary for ordered views, etc._

**Your Answer:**

---

## Comparers & String Handling

**Comparer choices:**  
_Explain what comparers you used and why (e.g., StringComparer.OrdinalIgnoreCase for keys)._

**Your Answer:**

**For keys:**

**For display sorting (if different):**

**Normalization rules:**  
_Describe how you normalize strings (trim whitespace, collapse duplicates, canonicalize casing)._

**Your Answer:**

**Bad key examples avoided:**  
_List examples of bad key choices and why you avoided them (e.g., non-unique names, culture-varying text, trailing spaces, substrings that can change)._

---

## Performance Considerations

**Expected data scale:**  
_Describe the expected size of your data (e.g., 100 items, 10,000 items)._

**Your Answer:**

**Performance bottlenecks identified:**  
_List any potential performance issues and how you addressed them._

**Your Answer:**

**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

**Your Answer:**

- Add:
- Search:
- List:
- Update:
- Delete:

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
_Explain major design choices and why you made them._

**Your Answer:**

**Tradeoffs made:**  
_Describe any tradeoffs between simplicity vs performance, memory vs speed, etc._

**Your Answer:**  
- Using List<Recipe> means searches are linear, but simplifies iteration and LINQ usage
- Recipe book stores only name + cooked state rather than full recipe objects
- Cooking logic favors simplicity over exact in-game accuracy

**What you would do differently with more time:**  
_Reflect on what you might change or improve._

**Your Answer:**  
- Separate data layer and UI logic more cleanly
- Improve recipe matching logic for more precise results
- Expand recipe metadata (hearts restored, effects, rarity)
