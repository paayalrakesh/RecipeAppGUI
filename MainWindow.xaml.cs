using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeAppGUI
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes; // Declaring a private list to store recipes

        public MainWindow() // Constructor for MainWindow
        {
            InitializeComponent(); // Initializing the window components
            recipes = new List<Recipe>(); // Initializing the recipes list
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e) // Event handler for adding a recipe
        {
            while (true) // Infinite loop to repeatedly ask for recipe details
            {
                string name = GetValidatedString("Please enter the name of this recipe:"); // Prompting user for recipe name
                if (name == null) return; // Exit if user cancels

                int numIngredients = GetValidatedNumber("Please enter the number of ingredients needed for this recipe:"); // Prompting user for number of ingredients
                if (numIngredients == -1) return; // Exit if user cancels

                int numSteps = GetValidatedNumber("Please enter how many steps are required for this recipe:"); // Prompting user for number of steps
                if (numSteps == -1) return; // Exit if user cancels

                Recipe recipe = new Recipe(name, numIngredients, numSteps); // Creating a new Recipe object

                for (int i = 0; i < numIngredients; i++) // Looping to get details for each ingredient
                {
                    MessageBox.Show($"Enter details for ingredient {i + 1}:"); // Informing user to enter ingredient details

                    string ingredientName = GetValidatedString("Name of Ingredient:"); // Prompting user for ingredient name
                    if (ingredientName == null) return; // Exit if user cancels

                    double quantity = GetValidatedDouble("Quantity:"); // Prompting user for ingredient quantity
                    if (quantity == -1) return; // Exit if user cancels

                    string unit = GetValidatedString("Unit of measurement:"); // Prompting user for unit of measurement
                    if (unit == null) return; // Exit if user cancels

                    int calories = GetValidatedNumber("Calories:"); // Prompting user for ingredient calories
                    if (calories == -1) return; // Exit if user cancels

                    string foodGroup = GetValidatedString("Food Group:"); // Prompting user for food group
                    if (foodGroup == null) return; // Exit if user cancels

                    recipe.AddIngredients(ingredientName, quantity, unit, calories, foodGroup); // Adding the ingredient to the recipe
                }

                /*Title:C# For Loop
               Author: w3schools
               Date: 2024
               Code Version: 1
               Availability: https://www.w3schools.com/cs/cs_for_loop.php */

                for (int i = 0; i < numSteps; i++) // Looping to get details for each step
                {
                    MessageBox.Show($"Enter details for step {i + 1}:"); // Informing user to enter step details

                    string stepDescription = GetValidatedString("Description:"); // Prompting user for step description
                    if (stepDescription == null) return; // Exit if user cancels

                    recipe.AddSteps(stepDescription); // Adding the step to the recipe

                    // Check if the user wants to clear fields after adding each step
                    if (AskToClearFields()) // Asking user if they want to clear fields
                    {
                        goto Restart; // Clear all fields and start over
                    }
                }

                recipe.OnCaloriesExceeded += message => MessageBox.Show(message); // Subscribing to calorie exceeded event
                recipes.Add(recipe); // Adding the recipe to the list

                MessageBox.Show("Recipe added successfully!"); // Informing user that recipe is added
                break; // Break the loop after successfully adding the recipe

            Restart:
                continue; // Restart the loop if fields are cleared
            }
        }

        private void ViewRecipesButton_Click(object sender, RoutedEventArgs e) // Event handler for viewing recipes
        {
            if (!recipes.Any()) // Checking if there are any recipes
            {
                MessageBox.Show("No recipes available."); // Informing user that there are no recipes
                return; // Exit if no recipes are available
            }

            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList(); // Sorting recipes alphabetically by name
            recipes = sortedRecipes; // Updating the recipes list with sorted recipes


            /*Title:C# For Loop
               Author: w3schools
               Date: 2024
               Code Version: 1
               Availability: https://www.w3schools.com/cs/cs_for_loop.php */

            string message = "Recipes:\n"; // Initializing message to display recipes
            for (int i = 0; i < sortedRecipes.Count; i++) // Looping through sorted recipes
            {
                message += $"{i + 1}. {sortedRecipes[i].Name}\n"; // Adding each recipe to the message
            }
            MessageBox.Show(message); // Displaying the recipes

            int recipeNumber = GetValidatedNumber("Please enter the number of the recipe you want to view:"); // Prompting user to enter recipe number
            if (recipeNumber == -1) return; // Exit if user cancels

            if (recipeNumber > 0 && recipeNumber <= sortedRecipes.Count) // Checking if recipe number is valid
            {
                sortedRecipes[recipeNumber - 1].DisplayRecipe(); // Displaying the selected recipe
                RecipeInteraction(sortedRecipes[recipeNumber - 1]); // Handling recipe interaction
            }
            else
            {
                MessageBox.Show("Invalid recipe number."); // Informing user of invalid recipe number
            }
        }

        private void FilterRecipesButton_Click(object sender, RoutedEventArgs e) // Event handler for filtering recipes
        {
            List<Recipe> filteredRecipes = FilterRecipes(); // Getting the filtered recipes
            if (!filteredRecipes.Any()) // Checking if there are any filtered recipes
            {
                MessageBox.Show("No recipes match the filter criteria.");
                return; // Exit if no filtered recipes are available
            }


            /*Title:C# For Loop
               Author: w3schools
               Date: 2024
               Code Version: 1
               Availability: https://www.w3schools.com/cs/cs_for_loop.php */

            string message = "Filtered Recipes:\n"; // Initializing message to display filtered recipes
            for (int i = 0; i < filteredRecipes.Count; i++) // Looping through filtered recipes
            {
                message += $"{i + 1}. {filteredRecipes[i].Name}\n"; // Adding each filtered recipe to the message
            }
            MessageBox.Show(message); // Displaying the filtered recipes

            int recipeNumber = GetValidatedNumber("Please enter the number of the recipe you want to view:"); // Prompting user to enter recipe number
            if (recipeNumber == -1) return; // Exit if user cancels

            if (recipeNumber > 0 && recipeNumber <= filteredRecipes.Count) // Checking if recipe number is valid
            {
                filteredRecipes[recipeNumber - 1].DisplayRecipe(); // Displaying the selected recipe
                RecipeInteraction(filteredRecipes[recipeNumber - 1]); // Handling recipe interaction
            }
            else
            {
                MessageBox.Show("Invalid recipe number."); // Informing user of invalid recipe number
            }
        }

        private List<Recipe> FilterRecipes() // Method to filter recipes
        {
            string filterChoice = GetValidatedString("Enter 'i' to filter by ingredient, 'f' to filter by food group, 'c' to filter by calories, or leave blank to view all recipes:");
            List<Recipe> filteredRecipes = recipes;

            if (filterChoice == "i")
            {
                string ingredient = GetValidatedString("Enter the ingredient to filter by:");
                if (ingredient == null) return new List<Recipe>(); // Handle cancel
                filteredRecipes = recipes.Where(r => r.Ingredients.Any(ing => ing.Name.ToLower().Contains(ingredient.ToLower()))).ToList();
            }
            else if (filterChoice == "f")
            {
                string foodGroup = GetValidatedString("Enter the food group to filter by:");
                if (foodGroup == null) return new List<Recipe>(); // Handle cancel
                filteredRecipes = recipes.Where(r => r.Ingredients.Any(ing => ing.FoodGroup.ToLower().Contains(foodGroup.ToLower()))).ToList();
            }
            else if (filterChoice == "c")
            {
                int maxCalories = GetValidatedNumber("Enter the maximum number of calories:");
                if (maxCalories == -1) return new List<Recipe>(); // Handle cancel
                filteredRecipes = recipes.Where(r => r.CalculateTotalCalories() <= maxCalories).ToList();
            }

            return filteredRecipes;
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e) // Event handler for scaling a recipe
        {
            if (!recipes.Any()) // Checking if there are any recipes
            {
                MessageBox.Show("No recipes available."); // Informing user that there are no recipes
                return; // Exit if no recipes are available
            }

            int recipeNumber = GetValidatedNumber("Enter the number of the recipe you want to scale:"); // Prompting user to enter recipe number
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= recipes.Count) // Checking if recipe number is valid
            {
                var selectedRecipe = recipes[recipeNumber - 1]; // Getting the selected recipe
                double factor = GetValidatedDouble("Enter a scaling factor (0.5, 2, or 3):"); // Prompting user for scaling factor
                if (factor == -1) return; // Handle cancel

                ScaleRecipe(selectedRecipe, factor); // Scaling the recipe
            }
            else
            {
                MessageBox.Show("Invalid recipe number."); // Informing user of invalid recipe number
            }
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e) // Event handler for resetting ingredient quantities
        {
            if (!recipes.Any()) // Checking if there are any recipes
            {
                MessageBox.Show("No recipes available."); // Informing user that there are no recipes
                return; // Exit if no recipes are available
            }

            int recipeNumber = GetValidatedNumber("Enter the number of the recipe you want to reset quantities for:"); // Prompting user to enter recipe number
            if (recipeNumber == -1) return; // Handle cancel

            /*Title:The foreach Loop
            Author: w3schools
            Date: 2024
            Code Version: 1
            Availability: https://www.w3schools.com/cs/cs_foreach_loop.php */

            if (recipeNumber > 0 && recipeNumber <= recipes.Count) // Checking if recipe number is valid
            {
                var selectedRecipe = recipes[recipeNumber - 1]; // Getting the selected recipe
                foreach (var ingredient in selectedRecipe.Ingredients) // Looping through ingredients to reset quantities
                {
                    ingredient.Quantity = ingredient.OriginalQuantity; // Resetting the quantity to original
                }
                selectedRecipe.DisplayRecipe(); // Displaying the updated recipe
            }
            else
            {
                MessageBox.Show("Invalid recipe number."); // Informing user of invalid recipe number
            }
        }

        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e) // Event handler for deleting a recipe
        {
            if (!recipes.Any()) // Checking if there are any recipes
            {
                MessageBox.Show("No recipes available."); // Informing user that there are no recipes
                return; // Exit if no recipes are available
            }

            int recipeNumber = GetValidatedNumber("Enter the number of the recipe you want to delete:"); // Prompting user to enter recipe number
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= recipes.Count) // Checking if recipe number is valid
            {
                var selectedRecipe = recipes[recipeNumber - 1]; // Getting the selected recipe
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the recipe?", "Confirmation", MessageBoxButton.YesNo); // Asking user for confirmation
                if (result == MessageBoxResult.Yes) // Checking if user confirmed
                {
                    recipes.Remove(selectedRecipe); // Removing the recipe from the list
                    MessageBox.Show("Recipe deleted."); // Informing user that recipe is deleted
                }
                else
                {
                    MessageBox.Show("Delete cancelled."); // Informing user that delete is cancelled
                }
            }
            else
            {
                MessageBox.Show("Invalid recipe number."); // Informing user of invalid recipe number
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) // Event handler for exiting the application
        {
            Application.Current.Shutdown(); // Shutting down the application
        }

        private string GetValidatedString(string prompt) // Method to get a validated string input
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(prompt); // Prompting user for input
            if (string.IsNullOrWhiteSpace(input)) // Checking if input is null or whitespace
            {
                return null; // Simulate cancel
            }
            return input; // Returning the input
        }

        private int GetValidatedNumber(string prompt) // Method to get a validated number input
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(prompt); // Prompting user for input
            if (string.IsNullOrWhiteSpace(input)) // Checking if input is null or whitespace
            {
                return -1; // Simulate cancel
            }

            if (int.TryParse(input, out int result)) // Trying to parse the input to an integer
            {
                return result; // Returning the parsed integer
            }

            MessageBox.Show("Invalid input. Please enter a valid number."); // Informing user of invalid input
            return GetValidatedNumber(prompt); // Recursively calling the method for valid input
        }

        private double GetValidatedDouble(string prompt) // Method to get a validated double input
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(prompt); // Prompting user for input
            if (string.IsNullOrWhiteSpace(input)) // Checking if input is null or whitespace
            {
                return -1; // Simulate cancel
            }

            if (double.TryParse(input, out double result)) // Trying to parse the input to a double
            {
                return result; // Returning the parsed double
            }

            MessageBox.Show("Invalid input. Please enter a valid number."); // Informing user of invalid input
            return GetValidatedDouble(prompt); // Recursively calling the method for valid input
        }

        private bool AskToClearFields() // Method to ask user if they want to clear fields
        {
            MessageBoxResult result = MessageBox.Show("Do you want to clear all fields and start over?", "Confirmation", MessageBoxButton.YesNo); // Asking user for confirmation

            if (result == MessageBoxResult.Yes) // Checking if user confirmed
            {
                MessageBox.Show("Are you sure you want to clear fields?", "Confirmation", MessageBoxButton.OK); // Asking user for final confirmation
                MessageBox.Show("Fields Cleared!"); // Informing user that fields are cleared
                return true; // Return true to indicate continuation
            }
            else
            {
                MessageBox.Show("Data clear cancelled.", "Cancelled", MessageBoxButton.OK); // Informing user that clear is cancelled
                return false; // Return false to indicate cancellation
            }
        }

         /*Title:The foreach Loop
             Author: w3schools
             Date: 2024
             Code Version: 1
             Availability: https://www.w3schools.com/cs/cs_foreach_loop.php */
        private void ScaleRecipe(Recipe recipe, double factor) // Method to scale a recipe
        {
            foreach (var ingredient in recipe.Ingredients) // Looping through ingredients to scale quantities
            {
                ingredient.Quantity *= factor; // Scaling the quantity by the factor
            }
            recipe.DisplayRecipe(); // Displaying the updated recipe
        }

        private void RecipeInteraction(Recipe selectedRecipe) // Method for recipe interaction
        {
            StepsPanel.Children.Clear(); // Clearing existing steps

            /*Title:C# For Loop
               Author: w3schools
               Date: 2024
               Code Version: 1
               Availability: https://www.w3schools.com/cs/cs_for_loop.php */

            for (int i = 0; i < selectedRecipe.Steps.Count; i++) // Looping through steps to create checkboxes
            {
                var checkBox = new CheckBox // Creating a new checkbox for each step
                {
                    Content = $"Step {i + 1}: {selectedRecipe.Steps[i]}", // Setting the content of the checkbox
                    Margin = new Thickness(5) // Setting the margin of the checkbox
                };
                StepsPanel.Children.Add(checkBox); // Adding the checkbox to the steps panel
            }
        }
    }
}