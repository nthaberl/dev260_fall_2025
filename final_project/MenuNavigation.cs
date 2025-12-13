using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace final_project
{
    public class MenuNavigation
    {
        //linear data structure storing all recipes loaded from JSON
        private List<Recipe> recipes;

        //non-linear

        //storing all unique ingredients loaded from JSON
        private HashSet<string> allIngredients;

        //storing current campfire ingredients
        private HashSet<string> campfire;

        //storing user's saved recipes
        private Dictionary<string, bool> recipeBook;

        public void Start()
        {
            //initializing data structures
            recipes = RecipeLoader.LoadRecipes();

            //initiliazing campfire and recipe book with case insensitive comparers
            campfire = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            recipeBook = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            allIngredients = GetAllIngredients();
            MainMenu();
        }

        private void MainMenu()
        {
            bool running = true;

            while (running)
            {
                Clear();
                WriteLine("\n\nWelcome to the Breath of the Wild Campfire!");
                WriteLine("In this app, you can view all recipes, search for specific ones,");
                WriteLine("cook using ingredients at your campfire, and manage your personal recipe book.\n\n");
                WriteLine("=== Breath of the Wild Recipe Helper ===\n");
                WriteLine("1. View All BOTW Recipes");
                WriteLine("2. Search For A Specific BOTW Recipe");
                WriteLine("3. Find out what you can cook in the campfire!");
                WriteLine("4. Manage Your Recipe Book");
                WriteLine("5. Quit");

                Write("\nChoose an option: ");
                string choice = ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        DisplayAllRecipes();
                        break;
                    case "2":
                        SearchRecipes();
                        break;
                    case "3":
                        CampfireMenu();
                        break;
                    case "4":
                        RecipeBookMenu();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        WriteLine("\nInvalid choice. Please try again.");
                        WriteLine("Press any key to try again.");
                        ReadKey();
                        break;
                }
            }
        }

        // ===================== RECIPES AND INGREDIENTS LOADED FROM JSON =====================

        //simple method for displaying all recipes that were loaded from JSON
        //intended as a reference material
        private void DisplayAllRecipes()
        {
            Clear();
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

            foreach (var recipe in sortedRecipes)
            {
                WriteLine(recipe.Name);
                foreach (var ingredients in recipe.Ingredients)
                {
                    WriteLine($"  - {ingredients.Replace("/", " / ")}");
                }
                WriteLine();
            }

            WriteLine($"There are {sortedRecipes.Count} recipes in total.\n");
            WriteLine("Press any key to return to main menu...");
            ReadKey();
        }

        //storing all possible ingredients, using hashtable to ensure uniqueness
        //using LINQ to flatten and process the ingredient groups
        private HashSet<string> GetAllIngredients()
        {
            return new HashSet<string>(
                recipes
                    .SelectMany(recipe => recipe.Ingredients)
                    .SelectMany(group => group.Split('/'))
                    .Select(ingredient => ingredient.Trim()),
                StringComparer.OrdinalIgnoreCase);
        }


        //allows users to search for recipes by full or partial name matches (not ingredient)
        //method is intended to be a quick reference for users
        private void SearchRecipes()
        {
            WriteLine("\nSearch for recipes by name.");
            WriteLine("Unsure what to look for? Try apple, soup, skewer, or cake!");
            Write("\nEnter recipe name to search: ");
            string search = ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(search))
            {
                WriteLine("Search term cannot be empty.");
                WriteLine("Press any key to return...");
                ReadKey();
                return;
            }

            var results = recipes
                .Where(recipe => recipe.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                WriteLine("No matching recipes found.");
                WriteLine("Press any key to return...");
                ReadKey();
            }
            else
            {
                WriteLine("\nSearch Results:\n");
                foreach (var recipe in results)
                {
                    WriteLine(recipe.Name);
                    foreach (var ingredients in recipe.Ingredients)
                    {
                        WriteLine($"  - {ingredients.Replace("/", " / ")}");
                    }
                    WriteLine();
                }

                WriteLine("Press any key to continue...");
                ReadKey();
            }

        }


        // ===================== CAMPFIRE MENU AND MANAGEMENT =====================

        private void CampfireMenu()
        {
            bool running = true;

            while (running)
            {
                Clear();
                WriteLine("🔥 Campfire 🔥");
                WriteLine("Here are all the possible ingredients you can add to the campire:\n");
                WriteLine("Campfire can contain a maximum of 5 unique ingredients at a time.\n");
                DisplayIngredientsTable();

                WriteLine("\n\nCurrently in the campfire:");
                WriteLine(campfire.Count == 0 ? "No ingredients! Add some to get started" : string.Join(", ", campfire));
                WriteLine();
                WriteLine("⭐ If you're not sure what to add, try cooking just an apple or razorshroom.");
                WriteLine("🌟 For something more substantial, try Raw Meat and Spicy Pepper.");
                WriteLine("🌟 If you're feeling extra fancy, try Bird Egg, Fresh Milk, Wildberry, Cane Sugar, or Tabantha Wheat!");
                WriteLine("\n1. Add Ingredient");
                WriteLine("2. Remove Ingredient");
                WriteLine("3. See what you can cook!");
                WriteLine("4. Clear Campfire");
                WriteLine("5. Return to Main Menu");

                Write("\nChoice: ");
                string choice = ReadLine();

                switch (choice)
                {
                    case "1":
                        AddIngredient();
                        break;
                    case "2":
                        RemoveIngredient();
                        break;
                    case "3":
                        Cook();
                        break;
                    case "4":
                        campfire.Clear();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        WriteLine("\nInvalid choice.");
                        WriteLine("Press any key to try again.");
                        ReadKey();
                        break;
                }
            }
        }


        //displaying all possible ingredients in app into easy to read table format
        //also shows users possible ingredients to add to campfire
        private void DisplayIngredientsTable()
        {
            var sortedIngredients = allIngredients.OrderBy(i => i).ToList();
            int columns = 4;
            int rows = (int)Math.Ceiling(sortedIngredients.Count / (double)columns);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    int index = row * columns + col;
                    if (index < sortedIngredients.Count)
                    {
                        string item = sortedIngredients[index];
                        Write(item.PadRight(25));
                    }
                }
                WriteLine();
            }
        }

        private void AddIngredient()
        {
            if (campfire.Count >= 5)
            {
                WriteLine("You can only have 5 ingredients in the campfire at a time!");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            Write("Ingredient name: ");
            string input = ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                WriteLine("Ingredient name cannot be empty.");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            //using helper method to validate ingredient
            if (!IsValidIngredient(input))
            {
                WriteLine($"'Unfortunately {input}' is not an ingredient in Breath of the Wild.");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            //HashSet.Add() returns false if item already exists
            if (!campfire.Add(input))
            {
                WriteLine("Ingredient already in campfire. You can only have 1 of each!");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            WriteLine("Ingredient added!");

        }

        private void RemoveIngredient()
        {
            Write("Ingredient to remove: ");
            string input = ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                WriteLine("Please enter an ingredient name.");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            if (campfire.Contains(input))
            {
                campfire.Remove(input);
                WriteLine($"{input} removed from the campfire.");
            }
            else
            {
                WriteLine($"{input} is not in the campfire.");
            }
        }


        //ingredient matching logic
        //determines what recipes can be cooked with current campfire ingredients
        //allows user to save cooked recipes into their recipe book
        private void Cook()
        {
            if (campfire.Count == 0)
            {
                WriteLine("Your campfire is empty! Add some ingredients first.");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            //storing cookable recipes into a list of recipe objects
            var cookable = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                //using helper method to determine if recipe can be cooked
                if (CanCook(recipe))
                {
                    //if so, add to "cookable" list
                    cookable.Add(recipe);
                }
            }

            //displaying recipes that can be created with ingredients in campfire

            //handling case where no recipes can be cooked
            if (cookable.Count == 0)
            {
                WriteLine("Unfortunately, you can't cook anything with the current ingredients.");
                WriteLine("You'd probably end up with something dubious...");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            //displaying cookable recipes
            WriteLine("You can cook:\n");
            foreach (var recipe in cookable)
            {
                WriteLine($"- {recipe.Name}");
            }

            //special case handling for only 1 cookable recipe
            //prompting user to save it directly instead of asking for name input

            if (cookable.Count == 1)
            {
                var singleRecipe = cookable[0];

                //preventing recipe from being save multiple times into recipe book
                if (recipeBook.ContainsKey(singleRecipe.Name))
                {
                    WriteLine($"\nYou have already saved '{singleRecipe.Name}' in your recipe book.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                    return;
                }
                Write($"\nWould you like to save '{singleRecipe.Name}'? (yes/no): ");
                string saveResponse = ReadLine().Trim().ToLower();

                if (saveResponse == "yes" || saveResponse == "y")
                {
                    recipeBook[singleRecipe.Name] = false; // false means not cooked yet, default value
                    WriteLine($"Recipe '{singleRecipe.Name}' saved to your recipe book.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                }
                else
                {
                    WriteLine("Recipe not saved.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                }
            }
            //handling case where multiple recipes can be cooked
            //prompting user to enter the full name of the recipe they want to save
            else
            {
                Write("\nEnter the full name of the recipe to save or press ENTER to skip: ");
                string recipeName = ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(recipeName))
                {
                    //using FirstOrDefault to find the recipe by name and return the object
                    var selectedRecipe = cookable.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                    //preventing recipe from being save multiple times
                    if (recipeBook.ContainsKey(selectedRecipe.Name))
                    {
                        WriteLine($"\nYou have already saved '{selectedRecipe.Name}' in your recipe book.");
                        WriteLine("Press any key to continue...");
                        ReadKey();
                        return;
                    }

                    if (selectedRecipe != null)
                    {
                        recipeBook[selectedRecipe.Name] = false; // false is the dictionary value, means not cooked yet
                        WriteLine($"Recipe '{selectedRecipe.Name}' saved to your recipe book.");
                        WriteLine("Press any key to continue...");
                        ReadKey();
                    }
                    else
                    {
                        WriteLine("Recipe not found in the cookable list.");
                        WriteLine("Press any key to continue...");
                        ReadKey();
                    }
                }
                else
                {
                    WriteLine("No recipe saved.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                }
            }

        }

        //helper methods for cooking logic

        //ensuring ingredient validity against all possible ingredients
        private bool IsValidIngredient(string input)
        {
            input = input.Trim();
            return recipes.Any(recipe => recipe.Ingredients
                .Any(group => group
                .Split('/')
                //checking each option in ingredient group for match
                .Any(option => string.Equals(option.Trim(), input, StringComparison.OrdinalIgnoreCase))
                ));
        }

        //determining if a recipe can be cooked with current campfire ingredients
        private bool CanCook(Recipe recipe)
        {
            foreach (var group in recipe.Ingredients)
            {
                var options = group.Split('/');

                //for each ingredient group, at least one option must be present in campfire
                if (!options.Any(opt => campfire.Contains(opt.Trim())))
                    return false;
            }
            return true;
        }

        // ===================== RECIPE BOOK MENU AND MANAGEMENT =====================

        private void RecipeBookMenu()
        {
            bool running = true;

            while (running)
            {
                Clear();
                WriteLine("📖 Recipe Book 📖");
                WriteLine("\n1. View Saved Recipes");
                WriteLine("2. Edit Saved Recipes");
                WriteLine("3. Delete Saved Recipes");
                WriteLine("4. Return to Main Menu");

                Write("\nChoice: ");
                string choice = ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        ViewRecipeBook();
                        break;
                    case "2":
                        EditRecipeBook();
                        break;
                    case "3":
                        DeleteRecipe();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        WriteLine("\nInvalid choice.");
                        WriteLine("Press any key to try again.");
                        ReadKey();
                        break;
                }
            }
        }

        private void ViewRecipeBook()
        {
            if (recipeBook.Count == 0)
            {
                WriteLine("You haven't saved any recipes, go to the campfire to cook something!");
                WriteLine("Press any key to continue...");
                ReadKey();
            }
            else
            {
                foreach (var recipe in recipeBook)
                {
                    string hasCooked = recipe.Value ? "[Cooked]" : "[Not Cooked]";
                    WriteLine($"{recipe.Key} {hasCooked}");
                }
                WriteLine("Press any key to continue...");
                ReadKey();
            }
        }

        //allowing users to mark recipes as cooked or not cooked to reflect if they have been cooked in game
        private void EditRecipeBook()
        {
            if (recipeBook.Count == 0)
            {
                WriteLine("You haven't saved any recipes, go to the campfire to cook something!");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            WriteLine("Your saved recipes:");
            foreach (var recipe in recipeBook)
            {
                string hasCooked = recipe.Value ? "[Cooked]" : "[Not Cooked]";
                WriteLine($"{recipe.Key} {hasCooked}");
            }

            Write("Full recipe name to edit: ");
            string name = ReadLine()?.Trim() ?? ""; ;

            if (recipeBook.ContainsKey(name))
            {
                string currentStatus = recipeBook[name] ? "cooked" : "not cooked";
                WriteLine($"The recipe '{name}' is currently marked as {currentStatus}.");
                Write("Would you like to change it? (yes/no): ");
                string cookedInput = ReadLine()?.Trim().ToLower() ?? "";
                if (cookedInput == "yes" || cookedInput == "y")
                {
                    recipeBook[name] = !recipeBook[name];
                    WriteLine($"{name} status changed to {(recipeBook[name] ? "cooked" : "not cooked")}.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                }
                else if (cookedInput == "no" || cookedInput == "n")
                {
                    WriteLine($"{name} status was left as {(recipeBook[name] ? "cooked" : "not cooked")}.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                }
                else
                {
                    WriteLine("Invalid input. No changes made.");
                    WriteLine("Press any key to continue...");
                    ReadKey();
                }
            }
            else
            {
                WriteLine("Recipe not found in your recipe book.");
                WriteLine("Press any key to continue...");
                ReadKey();
            }
        }

        private void DeleteRecipe()
        {
            if (recipeBook.Count == 0)
            {
                WriteLine("You haven't saved any recipes, go to the campfire to cook something!");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            Write("Enter the name of the recipe to delete: ");
            string name = ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(name))
            {
                WriteLine("Recipe name cannot be empty.");
                WriteLine("Press any key to continue...");
                ReadKey();
                return;
            }

            if (recipeBook.Remove(name))
            {
                WriteLine($"'{name}' deleted from your recipe book.");
                WriteLine("Press any key to continue...");
                ReadKey();
            }
            else
            {
                WriteLine($"'{name}' not found in your recipe book.");
                WriteLine("Press any key to continue...");
                ReadKey();
            }
        }

    }
}

