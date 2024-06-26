﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeAppGUI
{
    class Recipe
    {
        /*Title:How to initialize a list of strings (List<string>) with many string values
            Author: StackOverflow
            Date: 23 September 2019
            Code Version: 1
            Availability: https://stackoverflow.com/questions/3139118/how-to-initialize-a-list-of-strings-liststring-with-many-string-values */

        public string Name { get; set; } // Property for recipe name
        public int NumIngredients { get; set; } // Property for number of ingredients
        public int NumSteps { get; set; } // Property for number of steps
        public List<Ingredients> Ingredients { get; set; } // List to store ingredients
        public int TotalCalories => Ingredients.Sum(i => i.Calories); // Add TotalCalories property
        public List<string> Steps { get; set; } // List to store steps

        // Food group options with explanations
        private Dictionary<string, string> FoodGroupExplanations = new Dictionary<string, string>
        {
            { "Vegetables", "Rich in vitamins and minerals, and low in calories." },
            { "Fruits", "Provide essential vitamins, minerals, and fiber." },
            { "Grains", "Good source of energy and fiber." },
            { "Proteins", "Build and repair tissues, and are essential for muscle growth." },
            { "Dairy", "Rich in calcium and important for bone health." },
            { "Fats & Oils", "Provide essential fatty acids and help absorb fat-soluble vitamins." },
            { "Sweets", "High in sugar and calories, should be consumed in moderation." }
        };

        public Recipe(string name, int numIngredients, int numSteps) // Constructor to initialize a recipe
        {
            Name = name; // Setting the name of the recipe
            NumIngredients = numIngredients; // Setting the number of ingredients
            NumSteps = numSteps; // Setting the number of steps
            Ingredients = new List<Ingredients>(); // Initializing the list of ingredients
            Steps = new List<string>(); // Initializing the list of steps
        }

        public void AddIngredients(string name, double quantity, string unit, int calories, string foodGroup) // Method to add ingredients to the recipe
        {
            Ingredients.Add(new Ingredients { Name = name, Quantity = quantity, OriginalQuantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup }); // Creating a new ingredient object and adding it to the list of ingredients
        }

        public void AddSteps(string description) // Method to add steps to the recipe
        {
            Steps.Add(description); // Adding the step description to the list of steps
        }

        public void DisplayRecipe() // Method to display the recipe details
        {
            var recipeDetails = new StringBuilder();

            recipeDetails.AppendLine("-----------------------------------------------------------------");
            recipeDetails.AppendLine($"{Name} Recipe:");
            recipeDetails.AppendLine("-----------------------------------------------------------------");
            recipeDetails.AppendLine("This recipe contains the following ingredients:");

            foreach (var ingredient in Ingredients)
            {
                recipeDetails.AppendLine($"Ingredient: {ingredient.Name}");
                recipeDetails.AppendLine($"Quantity: {ingredient.Quantity} {ingredient.Unit}");
                recipeDetails.AppendLine($"Calories: {ingredient.Calories}");
                recipeDetails.AppendLine($"Food Group: {ingredient.FoodGroup}");
                recipeDetails.AppendLine();
            }

            recipeDetails.AppendLine("-----------------------------------------------------------------");
            recipeDetails.AppendLine("Steps to follow:");

            for (int i = 0; i < Steps.Count; i++)
            {
                recipeDetails.AppendLine($"Step: {i + 1}");
                recipeDetails.AppendLine(Steps[i]);
                recipeDetails.AppendLine();
            }

            recipeDetails.AppendLine("-----------------------------------------------------------------");

            int totalCalories = CalculateTotalCalories();
            recipeDetails.AppendLine("Total Calories: " + totalCalories);

            if (totalCalories > 300)
            {
                recipeDetails.AppendLine("Warning: Total calories exceed 300!");
            }

            string calorieMessage;
            if (totalCalories < 300)
            {
                calorieMessage = "This is a low-calorie meal.";
            }
            else
            {
                calorieMessage = $"This is a high-calorie meal. It exceeds the limit by {totalCalories - 300} calories.";

                var highCalorieIngredients = Ingredients.Where(i => i.Calories > 100).ToList();
                if (highCalorieIngredients.Any())
                {
                    calorieMessage += "\nHigh-calorie ingredients:";
                    foreach (var ingredient in highCalorieIngredients)
                    {
                        calorieMessage += $"\n- {ingredient.Name}: {ingredient.Calories} calories (Food Group: {ingredient.FoodGroup})";
                    }
                }
            }

            recipeDetails.AppendLine($"\n\nCalorie Information:\n{calorieMessage}");

            // Append food group explanations
            recipeDetails.AppendLine("\nFood Group Explanations:");
            foreach (var foodGroup in FoodGroupExplanations)
            {
                recipeDetails.AppendLine($"- {foodGroup.Key}: {foodGroup.Value}");
            }

            MessageBox.Show(recipeDetails.ToString(), "Recipe Details", MessageBoxButton.OK);
        }

        public int CalculateTotalCalories() // Method to calculate the total calories of the recipe
        {
            return Ingredients.Sum(ingredient => ingredient.Calories); // Summing up the calories of all ingredients
        }

        // Event for calories exceeded
        public delegate void CaloriesExceededHandler(string message); // Declaring a delegate for calories exceeded event
        public event CaloriesExceededHandler OnCaloriesExceeded; // Declaring the calories exceeded event
    }
}