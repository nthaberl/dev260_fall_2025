# Project Title

> One-sentence summary of what this app does and who it's for.  

A console app for Breath of the Wild players to view, search, and manage cooking recipes using ingredients they have in their campfire. 

---

## What I Built (Overview)

**Problem this solves:**  
_Explain the real-world task your app supports and why it's useful (2–4 sentences)._

**Your Answer:**  
This app helps players of the Breath of the Wild game easily find and manage cooking recipes.  
For those familiar with the game, cooking is a major mechanic in the game, however, recipes are hidden throughout the game making it difficult to heal early on.  
Later in-game users may be nervous to waste rare ingredients trying to craft something useful.  
Users can use this app as a quick reference to see what they can make with ingredients they have on hand, and for the completionists, track recipes so they can verify if they've been cooked already or not.

**Core features:**  
_List the main features your application provides (Add, Search, List, Update, Delete, etc.)_

**Your Answer:**

- View all recipes alphabetically with ingredient lists
- Search for recipes by partial or full name match
- Manage a "campfire", where users can add/remove up to 5 unique ingredients
- See which recipes can be made with current campfire ingredients
- Save recipes to a personal recipe cook with "cooked"/"not cooked" status for easy tracking
- Edit and save cooked status of each recipe saved in the recipebook

## How to Run

**Requirements:**  
_List required .NET version, OS requirements, and any dependencies._

**Your Answer:**  
- .NET 
- Compatible with Windows, macOS, or Linux command lines
- no additional external dependencies

```bash
git clone <your-repo-url>
cd <your-folder>
dotnet build
```

**Run:**  
_Provide the command to run your application._

**Your Answer:**

```bash
dotnet run
```

**Sample data (if applicable):**  
_Describe where sample data lives and how to load it (e.g., JSON file path, CSV import)._

**Your Answer:**

--- Sample recipes are loaded from a JSON file (recipes.json) located in the project directory. This file is loaded automatically when the app starts.

## Using the App (Quick Start)

**Typical workflow:**  
_Describe the typical user workflow in 2–4 steps._

**Your Answer:**

1. Launch app and choose from main menu options
2. Can view or search recipes for ideas
3. Add ingredients to campfire (up to 5)
4. Check what recipes can be created and save to recipe book

**Input tips:**  
_Explain case sensitivity, required fields, and how common errors are handled gracefully._

**Your Answer:**  
- Recipe and ingredient name inputs are case-insensitive
- Campfire ingredient list only accepts Breath of the Wild ingredients that appear in-game
- Inputs are trimmed of whitespace and validated before processing
- App provides clear prompts and messages to handle invalid/empty inputs gracefully
---

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**  
_List each data structure and briefly explain what feature it powers._

**Your Answer:**

- `List<Recipe>` → Stores all loaded recipes from JSON
- `HashSet<string>` → Maintains unique sets of ingredients (allIngredients and campfire) with case-insensitive comparison
- `Dictionary<string, bool>` → Maps saved recipe names to cooked status (Key: Recipe Name, Value: cookedStatus)
- _(Add others: Queue, Stack, SortedDictionary, custom BST/Graph, etc.)_

---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.

**Test scenarios:**  
_Describe each test scenario with steps and expected results._

**Your Answer:**

**Scenario 1: [Add and Remove Ingredients]**

- Steps: Add valid ingredients, attempted to add duplicates, remove existing and non-existing ingredients
- Expected result: Ingredients added if valid and unique; duplicates rejected; removal reflects correctly
- Actual result: Works as expected with proper messages

**Scenario 2: [Search Recipes by Partial Name]**

- Steps: Search with keywords like "apple" or "cake".
- Expected result: Recipes containing the search term display correctly.
- Actual result: Partial and case-insensitive matches found and displayed.

**Scenario 3: [Cooking Recipes with Campfire Ingredients]**

- Steps: Add ingredients, check cookable recipes, save a recipe.
- Expected result: Only recipes with all ingredient groups satisfied are shown; saving updates recipe book.
- Actual result: Recipes that includes ingredients are included, not strict matching. Other behaviors work as intended.

**Scenario 4: [Manage Recipe Book] (optional)**

- Steps: View saved recipes, mark cooked/not cooked, delete recipes.
- Expected result: Recipe book updates accordingly; status changes persist.
- Actual result: Functions as intended.

---

## Known Limitations

**Limitations and edge cases:**  
_Describe any edge cases not handled, performance caveats, or known issues._

**Your Answer:**
This app is limited to basic food recipes only, variants on basic recipes and elixirs (which require monster parts) are not included.  
Since there is no definitive guide, I created the JSON manually to store only the information I needed. Unfortunately this unique case will require manual editing to support more content or features.

## Comparers & String Handling

**Keys comparer:**  
_Describe what string comparer you used (e.g., StringComparer.OrdinalIgnoreCase) and why._

**Your Answer:**  
Case-insensitive comparisons are done with StringComparer.OrdinalIgnoreCase to allow flexible input matching regardless of user capitalization.
Most apps utilize this built in StringComparer.

**Normalization:**  
_Explain how you normalize strings (trim whitespace, consistent casing, duplicate checks)._

**Your Answer:**  
All input strings are trimmed of whitespace before processing to avoid errors. Duplicate entries are prevented using case-insensitive sets and dictionaries.

---

## Credits & AI Disclosure

**Resources:**  
_List any articles, documentation, or code snippets you referenced or adapted._

**Your Answer:**

-
- **AI usage (if any):**  
   _Describe what you asked AI tools, what code they influenced, and how you verified correctness._

  **Your Answer:**  
- Breath of the Wild recipe databases and fan wikis for recipe data cross-reference.
- Microsoft Documentation, Reddit, and StackOverflow for .NET collections and data handling/manipulation
- Used AI tools to help with RecipeLoader.cs, verified through manual testing and debugging

  ***

## Challenges and Solutions

**Biggest challenge faced:**  
_Describe the most difficult part of the project - was it choosing the right data structures, implementing search functionality, handling edge cases, designing the user interface, or understanding a specific algorithm?_

**Your Answer:**  
The most difficult part was actually the scope of the project. I tried going in many different directions before dialing the features back.  
The recipes in Breath of the Wild are pretty complex, so trying to mimic the full functionality first try would be a monumental task.  

**How you solved it:**  
_Explain your solution approach and what helped you figure it out - research, consulting documentation, debugging with breakpoints, testing with simple examples, refactoring your design, etc._

**Your Answer:**  
When I felt lost in my own code, I knew it was time to rethink my app. This included refactoring my design and rewriting my entire JSON file (would not recommend this experience). 

**Most confusing concept:**  
_What was hardest to understand about data structures, algorithm complexity, key comparers, normalization, or organizing your code architecture?_

**Your Answer:**  
I had difficulty trying to organize my code. I started out by having classes separated out, however this ended up being more confusing. I kept all functionality in one file, but in the future I would break this up into logical components. 

## Code Quality

**What you're most proud of in your implementation:**  
_Highlight the best aspect of your code - maybe your data structure choices, clean architecture, efficient algorithms, intuitive user interface, thorough error handling, or elegant solution to a complex problem._

**Your Answer:**  
I'm proud of how relatively simple I was able to keep the program and the data structures I used. They're pretty straightforward to implement but I only had minimal exposure to them in the past so I was glad  Icould implement them!

**What you would improve if you had more time:**  
_Identify areas for potential improvement - perhaps adding more features, optimizing performance, improving error handling, adding data persistence, refactoring for better maintainability, or enhancing the user experience._

**Your Answer:**

This application has a lot of potential! Additional recipes as well as more complex recipes could be added. Unfortunately I had to write most of the JSON file and in its current state it's pretty simple.  
Every entry would have to be manually edited to support additional features.

## Real-World Applications

**How this relates to real-world systems:**  
_Describe how your implementation connects to actual software systems - e.g., inventory management, customer databases, e-commerce platforms, social networks, task managers, or other applications in the industry._

**Your Answer:**  
This app is a simplified example of inventory and recipe management systems common in gaming but also shows up in restaurant software and supply chain logistics. It demonstrates how data structures enable efficient searching, filtering, and user customization in practical applications.

**What you learned about data structures and algorithms:**  
_What insights did you gain about choosing appropriate data structures, performance tradeoffs, Big-O complexity in practice, the importance of good key design, or how data structures enable specific features?_

**Your Answer:**  
Choosing the appropriate data structure really came down to their use case in my app, however my app is not using a massive amount of data so performance trade-off was not a huge critera, I focused on using structures that would be natural fits in my app. 

## Submission Checklist

- [ ] Public GitHub repository link submitted
- [ ] README.md completed (this file)
- [ ] DESIGN.md completed
- [ ] Source code included and builds successfully
- [ ] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):**
