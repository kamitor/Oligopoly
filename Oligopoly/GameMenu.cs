﻿namespace Oligopoly
{
    public class GameMenu : Menu
    {
        // Create a class fields.
        private int CurrentEvent;
        private Data? Data;

        /// <summary>
        /// Initializes a new instance of the GameMenu class with given parameters.
        /// </summary>
        /// <param name="prompt">The prompt to display above the menu.</param>
        /// <param name="options">The options to display in the menu.</param>
        /// <param name="outputDelay">The text output delay. Have to be a positive integer or zero.</param>
        /// <param name="currentEvent">The integer that represents current event.</param>
        /// <param name="data">The Data class object, which contains all companies and events.</param>
        public GameMenu(string prompt, string[] options, int outputDelay, int currentEvent, Data? data) : base (prompt, options, outputDelay)
        {
            CurrentEvent = currentEvent;
            Data = data;
        }

        /// <summary>
        /// Displays menu to the console and redraws it when user select other option.
        /// </summary>
        public override void DisplayMenu()
        {
            // Display current event.
            Console.WriteLine($"{Data.gameEvents[CurrentEvent].Title}");
            Console.WriteLine($"\n{Data.gameEvents[CurrentEvent].Content}\n");

            // Display companies.
            Console.Write("╔");
            for (int i = 0; i < 120 -2; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╗");

            Console.WriteLine(String.Format($"{"║ Company",-72} ║ {"Ticker",8} ║ {"Industry",10} ║ {"Share Price",19} ║"));

            Console.Write("╚");
            for (int i = 0; i < 120 - 2; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╝");

            Console.Write("╔");
            for (int i = 0; i < 120 - 2; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╗");

            foreach (var company in Data.gameCompanies)
            {
                Console.WriteLine(String.Format($"║ {company.Name,-70} ║ {company.Ticker,8} ║ {company.Industry,10} ║ {company.SharePrice,19} ║"));
            }

            Console.Write("╚");
            for (int i = 0; i < 120 - 2; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╝");

            // Display the prompt above the menu.
            foreach (char symbol in Prompt)
            {
                Thread.Sleep(OutputDelay);
                Console.Write(symbol);
            }

            // Display all options inside the menu and redraw it when user select other option.
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"[{prefix}] {currentOption}");
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Runs the menu.
        /// </summary>
        /// <returns>An integer that represents the selected option.</returns>
        public override int RunMenu()
        {
            // Create variable, that contains key that was pressed.
            ConsoleKey keyPressed;

            do
            {
                // Redraw the menu.
                Console.Clear();
                DisplayMenu();

                //Read the user's input.
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                // Get the key that was pressed.
                keyPressed = keyInfo.Key;

                // Move the selection up or down, based on the pressed key.
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;

                    // Wrap around if user is out of range.
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;

                    // Wrap around if user is out of range.
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            // Return the selected option.
            return SelectedIndex;
        }
    }
}