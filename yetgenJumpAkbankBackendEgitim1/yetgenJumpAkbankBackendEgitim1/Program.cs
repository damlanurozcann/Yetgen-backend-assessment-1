using System;
using System.Collections.Generic;

namespace YetgenJumpAkbankBackendTraining1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] persons = { "Damla", "Zubeyde", "Seyit", "Mehmet", "Ayten" };
            Dictionary<string, int> personBalance = new Dictionary<string, int>
            {
                {"Damla", 350 },
                {"Zubeyde", 500 },
                {"Seyit", 100 },
                {"Mehmet", 400 },
                {"Ayten", 300 }
            };

            string[] snacks = { "Crackers", "Cake", "Chips", "Granola Bars", "Popcorn" };
            Dictionary<string, int> snackBalance = new Dictionary<string, int>
            {
                {"Crackers", 100 },
                {"Cake", 500 },
                {"Chips", 100 },
                {"Granola Bars", 450 },
                {"Popcorn", 150 }
            };

            Console.WriteLine("Please select a person:");
            for (int i = 0; i < persons.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {persons[i]}");
            }

            int selectedPersonIndex = int.Parse(Console.ReadLine()) - 1;

            string selectedPerson = persons[selectedPersonIndex];
            int personBudget = personBalance[selectedPerson];

            Console.WriteLine($"{selectedPerson}'s balance: {personBudget} TL");
            Console.WriteLine("Available Snacks:");

            List<string> affordableSnacks = new List<string>();
            foreach (string snack in snacks)
            {
                int snackPrice = snackBalance[snack];
                if (personBudget >= snackPrice)
                {
                    affordableSnacks.Add(snack);
                    Console.WriteLine($"- {snack} ({snackPrice} TL)");
                }
            }

            if (affordableSnacks.Count == 0)
            {
                Console.WriteLine("No snack available for purchase.");
            }
            else
            {
                Console.WriteLine("Selectable combinations:");
                GenerateSnackCombinations(affordableSnacks, 0, new List<string>(), personBudget, snackBalance);
            }
        }

        static void GenerateSnackCombinations(List<string> snacks, int startIndex, List<string> currentCombination, int budget, Dictionary<string, int> snackBalance)
        {
            for (int i = startIndex; i < snacks.Count; i++)
            {
                int snackPrice = snackBalance[snacks[i]];
                if (budget >= snackPrice)
                {
                    currentCombination.Add(snacks[i]);
                    if (budget - snackPrice >= 0)
                    {
                        GenerateSnackCombinations(snacks, i, currentCombination, budget - snackPrice, snackBalance);
                    }
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }

            if (currentCombination.Count > 0)
            {
                int combinationTotalPrice = 0;
                string combinationText = string.Join(", ", currentCombination);

                foreach (string snack in currentCombination)
                {
                    int snackPrice = snackBalance[snack];
                    combinationTotalPrice += snackPrice;
                }

                Console.WriteLine($"{combinationText} - Total Price: {combinationTotalPrice} TL");
            }
        }
    }
}
