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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;
        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                string name = GetValidatedString("Please enter the name of this recipe:");
                if (name == null) return;

                int numIngredients = GetValidatedNumber("Please enter the number of ingredients needed for this recipe:");
                if (numIngredients == -1) return;

                int numSteps = GetValidatedNumber("Please enter how many steps are required for this recipe:");
                if (numSteps == -1) return;

                Recipe recipe = new Recipe(name, numIngredients, numSteps);

                for (int i = 0; i < numIngredients; i++)
                {
                    MessageBox.Show($"Enter details for ingredient {i + 1}:");

                    string ingredientName = GetValidatedString("Name of Ingredient:");
                    if (ingredientName == null) return;

                    double quantity = GetValidatedDouble("Quantity:");
                    if (quantity == -1) return;

                    string unit = GetValidatedString("Unit of measurement:");
                    if (unit == null) return;

                    int calories = GetValidatedNumber("Calories:");
                    if (calories == -1) return;

                    string foodGroup = GetValidatedString("Food Group:");
                    if (foodGroup == null) return;

                    recipe.AddIngredients(ingredientName, quantity, unit, calories, foodGroup);

                }

                for (int i = 0; i < numSteps; i++)
                {
                    MessageBox.Show($"Enter details for step {i + 1}:");

                    string stepDescription = GetValidatedString("Description:");
                    if (stepDescription == null) return;

                    recipe.AddSteps(stepDescription);

                    // Check if the user wants to clear fields after adding each step
                    if (AskToClearFields())
                    {
                        goto Restart; // Clear all fields and start over
                    }
                }

                recipe.OnCaloriesExceeded += message => MessageBox.Show(message);
                recipes.Add(recipe);

                MessageBox.Show("Recipe added successfully!");
             break; // Break the loop after successfully adding the recipe

    Restart:
        continue; // Restart the loop if fields are cleared
            }
        }

        private void ViewRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available.");
                return;
            }

            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            recipes = sortedRecipes;

            string message = "Recipes:\n";
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                message += $"{i + 1}. {sortedRecipes[i].Name}\n";
            }
            MessageBox.Show(message);

            int recipeNumber = GetValidatedNumber("Please enter the number of the recipe you want to view:");
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= sortedRecipes.Count)
            {
                sortedRecipes[recipeNumber - 1].DisplayRecipe();
                RecipeInteraction(sortedRecipes[recipeNumber - 1]);
            }
            else
            {
                MessageBox.Show("Invalid recipe number.");
            }
        }

        private void FilterRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Recipe> filteredRecipes = FilterRecipes();
            if (!filteredRecipes.Any())
            {
                MessageBox.Show("No recipes match the filter criteria.");
                return;
            }

            string message = "Filtered Recipes:\n";
            for (int i = 0; i < filteredRecipes.Count; i++)
            {
                message += $"{i + 1}. {filteredRecipes[i].Name}\n";
            }
            MessageBox.Show(message);

            int recipeNumber = GetValidatedNumber("Please enter the number of the recipe you want to view:");
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= filteredRecipes.Count)
            {
                filteredRecipes[recipeNumber - 1].DisplayRecipe();
                RecipeInteraction(filteredRecipes[recipeNumber - 1]);
            }
            else
            {
                MessageBox.Show("Invalid recipe number.");
            }
        }

        private List<Recipe> FilterRecipes()
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

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available.");
                return;
            }

            int recipeNumber = GetValidatedNumber("Enter the number of the recipe you want to scale:");
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= recipes.Count)
            {
                var selectedRecipe = recipes[recipeNumber - 1];
                double factor = GetValidatedDouble("Enter a scaling factor (0.5, 2, or 3):");
                if (factor == -1) return; // Handle cancel

                ScaleRecipe(selectedRecipe, factor);
            }
            else
            {
                MessageBox.Show("Invalid recipe number.");
            }
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available.");
                return;
            }

            int recipeNumber = GetValidatedNumber("Enter the number of the recipe you want to reset quantities for:");
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= recipes.Count)
            {
                var selectedRecipe = recipes[recipeNumber - 1];
                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    ingredient.Quantity = ingredient.OriginalQuantity;
                }
                selectedRecipe.DisplayRecipe();
            }
            else
            {
                MessageBox.Show("Invalid recipe number.");
            }
        }

        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available.");
                return;
            }

            int recipeNumber = GetValidatedNumber("Enter the number of the recipe you want to delete:");
            if (recipeNumber == -1) return; // Handle cancel

            if (recipeNumber > 0 && recipeNumber <= recipes.Count)
            {
                var selectedRecipe = recipes[recipeNumber - 1];
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the recipe?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    recipes.Remove(selectedRecipe);
                    MessageBox.Show("Recipe deleted.");
                }
                else
                {
                    MessageBox.Show("Delete cancelled.");
                }
            }
            else
            {
                MessageBox.Show("Invalid recipe number.");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private string GetValidatedString(string prompt)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(prompt);
            if (string.IsNullOrWhiteSpace(input))
            {
                return null; // Simulate cancel
            }
            return input;
        }

        private int GetValidatedNumber(string prompt)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(prompt);
            if (string.IsNullOrWhiteSpace(input))
            {
                return -1; // Simulate cancel
            }

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            MessageBox.Show("Invalid input. Please enter a valid number.");
            return GetValidatedNumber(prompt);
        }

        private double GetValidatedDouble(string prompt)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(prompt);
            if (string.IsNullOrWhiteSpace(input))
            {
                return -1; // Simulate cancel
            }

            if (double.TryParse(input, out double result))
            {
                return result;
            }

            MessageBox.Show("Invalid input. Please enter a valid number.");
            return GetValidatedDouble(prompt);
        }

        private bool AskToClearFields()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to clear all fields and start over?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Are you sure you want to clear fields?", "Confirmation", MessageBoxButton.OK);
                MessageBox.Show("Fields Cleared!");
                return true; // Return true to indicate continuation
            }
            else
            {
                MessageBox.Show("Data clear cancelled.", "Cancelled", MessageBoxButton.OK);
                return false; // Return false to indicate cancellation
            }

        }


        private void ScaleRecipe(Recipe recipe, double factor)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity *= factor;
            }
            recipe.DisplayRecipe();
        }

        private void RecipeInteraction(Recipe selectedRecipe)
        {
            // Clear existing steps
            StepsPanel.Children.Clear();

            // Create and display checkboxes for each step
            for (int i = 0; i < selectedRecipe.Steps.Count; i++)
            {
                var checkBox = new CheckBox
                {
                    Content = $"Step {i + 1}: {selectedRecipe.Steps[i]}",
                    Margin = new Thickness(5)
                };
                StepsPanel.Children.Add(checkBox);
            }
        }
    }
}