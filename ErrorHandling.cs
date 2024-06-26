using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppGUI
{
    class ErrorHandling
    {
        // Method to get a validated string input
        public static string GetValidatedString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt); // Prompting the user for input
                string input = Console.ReadLine(); // Reading user input
                if (!string.IsNullOrEmpty(input)) // Checking if input is not null or empty
                {
                    return input; // Returning input if it's valid
                }
                Console.WriteLine("Invalid input. Do not leave the field empty."); // Displaying error message for invalid input
            }
        }

        // Method to get a validated integer input
        public static int GetValidatedNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt); // Prompting the user for input
                if (int.TryParse(Console.ReadLine(), out int number) && number > 0) // Trying to parse input as integer
                {
                    return number; // Returning input if it's valid
                }
                Console.WriteLine("Invalid input. Please enter a positive number."); // Displaying error message for invalid input
            }
        }

        // Method to get a validated double input
        public static double GetValidatedDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt); // Prompting the user for input
                if (double.TryParse(Console.ReadLine(), out double number) && number > 0) // Trying to parse input as double
                {
                    return number; // Returning input if it's valid
                }
                Console.WriteLine("Invalid input. Please enter a positive number."); // Displaying error message for invalid input
            }
        }
    }
}
