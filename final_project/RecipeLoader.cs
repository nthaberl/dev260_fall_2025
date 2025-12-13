using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static System.Console;

namespace final_project
{
    public class Recipe
    {
        //name of recipe loaded from JSON
        public string? Name { get; set; }

        //list of ingredients loaded from JSON
        public List<string> Ingredients { get; set; }
    }

    public static class RecipeLoader
    {
        public static List<Recipe> LoadRecipes()
        {
            try
            {
                string json = File.ReadAllText("recipes.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var recipes = JsonSerializer.Deserialize<List<Recipe>>(json, options);

                //return recipes otherwise an empty list
                //empty list would only occur here is JSON deserialization fails
                return recipes ?? new List<Recipe>(); 
            }
            catch (Exception ex)
            {
                //catch any other errors like incorrect file path, invalid JSON, etc.
                WriteLine($"Error loading recipes: {ex.Message}");
                return new List<Recipe>();
            }
        }
    }
}