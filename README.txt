Welcome to My Recip-Ebook App! This is a Windows Presentation Foundation (WPF) application for storing, scaling, resetting, and clearing recipes. It allows users to manage an unlimited number of recipes, each with ingredients, steps, and nutritional information. Users can add new recipes, view all recipes sorted alphabetically, and select specific recipes to display. Each ingredient includes details such as name, quantity, unit, calories, and food group. The application calculates and displays the total calories for each recipe, notifying the user if the total exceeds 300 calories.

Changes made to code in POE: My original code was extended to support the following new features:

* Check List: After viewing a recipe, users are provided with an onscreen checklist to tick off steps as they go.
* User Interface: The app has been updated from a Console application to a WPF application to allow a friendly user interface and usage options, such as selecting certain requests instead of typing each requirement out.
* Filtering by groups: The app can now filter recipes to the user by its food group, max calorie content and its ingredients.

In part 2, I did not include an explanation that is specific to certain ranges of calories, I also did not display an alert showing by how many calories the recipe is over and which food group is causing this.
In my POE I have corrected the above issues, and my app now displays information about the calories and it tells the user whether it is a low or high calorie meal and displays information about the food groups to provide a better understanding to users about what each group is.

Below is how to install, run and use my app and it highlights the new features of the updated app.

Prerequisites:
Visual Studio 2019 or later
.NET 5.0 or later

Installation Instructions: 

1.Clone the Repository
Open a terminal or command prompt.
Clone the repository from GitHub: git clone https://github.com/paayalrakesh/RecipeAppGUI.git

2.Open in Visual Studio
Open Visual Studio.
Select File > Open > Project/Solution.
Navigate to the cloned repository folder and select RecipeAppGUI.sln.

3.Build and Run
Once the solution is loaded, build the project by selecting Build > Build Solution from the menu or pressing Ctrl+Shift+B.
Run the application by selecting Debug > Start Debugging from the menu or pressing F5.

Features of the app

Entering Recipe Details The user can enter the details for multiple recipes, including the number of ingredients and steps. For each ingredient, the user can provide the name, quantity, and unit of measurement, calorie content and its food group. For each step, the user can provide a description of what needs to be done.

Displaying the Recipe 
* The app will display the full recipe, including the ingredients and steps, in a neat format to the user.

Filtering the Recipe
* The user can choose to filter which recipes they want to view by entering the category they wish to view it by such as maximum calories(c), food groups(f) and ingredients(i)

Scaling the Recipe 
* The user can request that the recipe is scaled by a factor of 0,5, 2, or 3. All the ingredient quantities will be changed accordingly when the recipe is displayed. For example, if the original recipe calls for one tablespoon of sugar, it will become two tablespoons if the factor is 2.

Resetting Quantities 
* The user can request that the quantities be reset to the original values. This will revert any scaling changes made to the recipe.

Clearing Data 
* The user can clear all the data to enter a new recipe. This will remove any previously entered recipe details.

Deleting a recipe
* The user will be able to permanently remove a recipe stored in memory.

Non-functional Requirements

* Coding Standards The app follows internationally acceptable coding standards. Comprehensive comments are provided to explain variable names, methods, and the logic of the programming code.

* Use of Classes The app utilizes classes to organize and structure the code.

* Storage The user data is stored in memory while the software is running. The data is not persisted between runs.

Usage
* To use the Recip-Ebook App, follow these steps: 
* A list of options is present to you, and you may select any of the options that applicable to you eg. “Add a new recipe”. 
* Enter the details for that recipe, including the number of ingredients and steps. For each ingredient, provide the name, quantity, calories, food groups and unit of measurement. 
* For each step, provide a description of what needs to be done. If you make a mistake and wish to clear your fields and start over, you will be given the choice to clear the fields and start over again. If you do not wish to, you can select “no” and continue as normal.
* The app will add the recipe to storage.
* You can choose to view your recipes from a list.
* To scale the recipe, select the “scale” option and enter a scaling factor of 0,5, 2, or 3. The ingredient quantities will be adjusted accordingly when the recipe is displayed. 
* To reset the quantities to the original values, use the reset option. 
* To delete a recipe from memory you can select the option from the menu and the data will be deleted. 
* You can close the app by selecting the exit button.

That's it! You are now ready to use the Recipe App to manage your recipes.
