using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppGUI
{
    class Ingredients
    {
        public string Name { get; set; } // Property for the name of the ingredient
        public double Quantity { get; set; } // Property for the quantity of the ingredient
        public double OriginalQuantity { get; set; } // Property for the original quantity of the ingredient
        public string Unit { get; set; } // Property for the unit of measurement of the ingredient
        public int Calories { get; set; } // Property for the calories of the ingredient
        public string FoodGroup { get; set; } // Property for the food group of the ingredient
    }
}
