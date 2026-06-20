using System;
using System.Diagnostics;

namespace GuessTheNumber2
{
    public class GameEngine
    {
        private readonly GameConfiguration _config;
        private readonly HallOfFame _hof;
        private readonly Random _random = new();

        private readonly string[] TooLowMessagesPL = { "Za mało!", "Spróbuj wyżej.", "Nisko... celuj wyżej!", "Trochę brakuje, daj więcej.", "Podłoga! Zwiększ wartość." };
        private readonly string[] TooHighMessagesPL = { "Za dużo!", "Przesadziłeś, schodź w dół.", "Cofnij się, za wysoka liczba.", "Sufit! Zmniejsz wartość.", "Za bogato, daj mniej." };
        private readonly string[] TooLowMessagesEN = { "Too low!", "Try higher.", "Aim higher!", "Not enough, add more.", "Increase the value." };
        private readonly string[] TooHighMessagesEN = { "Too high!", "Go lower.", "Aim lower!", "Too much, decrease value.", "Lower your target." };

        public GameEngine(GameConfiguration config, HallOfFame hof)
        {
            _config = config;
            _hof = hof;
        }

        public void StartNewGame()
        {
            // 1. Wybór poziomu trudności strzałkami
            string[] diffOptions = { LanguageManager.Get("Easy"), LanguageManager.Get("Medium"), LanguageManager.Get("Hard") };
            int selectedDiff = SetSelectionMenu(LanguageManager.Get("SelectDifficulty"), diffOptions);

            string difficultyName = "Medium";
            int maxNumber = 100;
            if (selectedDiff == 0) { difficultyName = "Easy"; maxNumber = 50; }
            else if (selectedDiff == 2) { difficultyName = "Hard"; maxNumber = 250; }

            // 2. Wybór trybu gry strzałkami
            string[] modeOptions = { LanguageManager.Get("StandardGame"), LanguageManager.Get("NewGamePlus") };
            int selectedMode = SetSelectionMenu(LanguageManager.Get("GameMode"), modeOptions);
            bool isNgPlus = selectedMode == 1;

            // 3. Tryb zakładu
            bool isBetMode = false;
            int maxAttempts = int.MaxValue;

            if (!isNgPlus && _config.AskForBet)
            {
                Console.Clear();
                Console.Write(LanguageManager.Get("BetPrompt") + " ");
                string betAns = Console.ReadLine()?.ToLower();
                if (betAns == "t" || betAns == "y")
                {
                    isBetMode = true;
                    Console.Write(LanguageManager.Get("BetAttempts") + " ");
                    int.TryParse(Console.ReadLine(), out maxAttempts);
                }
            }

            RunGameplay(maxNumber, difficultyName, isNgPlus, maxAttempts);
        }

        private int SetSelectionMenu(string title, string[] options)
        {
            int currentSelection = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{title}\n");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" -> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {options[i]}");
                    }
                }

                var key = Console.ReadKey(true).Key;
                try { Console.Beep(500, 40); } catch { }

                if (key == ConsoleKey.UpArrow) currentSelection = (currentSelection - 1 + options.Length) % options.Length;
                else if (key == ConsoleKey.DownArrow) currentSelection = (currentSelection + 1) % options.Length;
                else if (key == ConsoleKey.Enter) return currentSelection;
            }
        }

        private void RunGameplay(int maxNumber, string difficulty, bool isNgPlus, int maxAttempts)
        {
            int secretFileNumber = _random.Next(1, maxNumber + 1);
            int currentAttempt = 1;
            bool guessed = false;
            int rerollInterval = _random.Next(6, 9);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (currentAttempt <= maxAttempts && !guessed)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($" 🎯 [{difficulty.ToUpper()}] " + (isNgPlus ? "NEW GAME PLUS" : "STANDARD MODE"));
                Console.ResetColor();
                Console.WriteLine($" 📊 {LanguageManager.Get("Attempt")}: {currentAttempt} / {(maxAttempts == int.MaxValue ? "∞" : maxAttempts.ToString())}\n");
                Console.Write($" 🔍 {LanguageManager.Get("EnterNumber")}");

                if (!int.TryParse(Console.ReadLine(), out int userGuess))
                {
                    currentAttempt++;
                    continue;
                }

                if (userGuess == secretFileNumber)
                {
                    guessed = true;
                    stopwatch.Stop();
                    int timeTaken = (int)stopwatch.Elapsed.TotalSeconds;

                    // Dźwięk wygranej (fanfara!)
                    try { Console.Beep(600, 150); Console.Beep(800, 150); Console.Beep(1000, 400); } catch { }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n ✨ {LanguageManager.Get("Win")} ✨");
                    Console.WriteLine($" 🕒 {string.Format(LanguageManager.Get("Time"), timeTaken)}");
                    Console.ResetColor();

                    Console.Write($"\n 👤 {LanguageManager.Get("EnterName")}");
                    string name = Console.ReadLine();
                    _hof.AddScore(new ScoreModel(name, currentAttempt, timeTaken, difficulty, isNgPlus));
                    break;
                }
                else
                {
                    string msg;
                    bool isPl = LanguageManager.CurrentLanguage == Language.PL;

                    if (userGuess < secretFileNumber)
                    {
                        msg = isPl ? TooLowMessagesPL[_random.Next(5)] : TooLowMessagesEN[_random.Next(5)];
                        try { Console.Beep(350, 150); } catch { } // Niski dźwięk przy za niskiej liczbie
                    }
                    else
                    {
                        msg = isPl ? TooHighMessagesPL[_random.Next(5)] : TooHighMessagesEN[_random.Next(5)];
                        try { Console.Beep(550, 150); } catch { } // Wysoki dźwięk przy za wysokiej liczbie
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n ❌ {msg}");
                    Console.ResetColor();

                    if (isNgPlus && currentAttempt % rerollInterval == 0)
                    {
                        secretFileNumber = _random.Next(1, maxNumber + 1);
                        // Dodatkowy sygnał alarmowy dla przelosowania liczby
                        try { Console.Beep(880, 80); Console.Beep(880, 80); } catch { }

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(LanguageManager.CurrentLanguage == Language.PL
                            ? "\n 🌀 ![NG+] Ukryta liczba została właśnie przelosowana!"
                            : "\n 🌀 ![NG+] The hidden number has just been rerolled!");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\n [Kliknij dowolny klawisz, aby kontynuować...] ");
                    Console.ReadKey(true);
                    currentAttempt++;
                }
            }

            if (!guessed)
            {
                try { Console.Beep(200, 600); } catch { } // Dźwięk porażki
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n {LanguageManager.Get("GameOver")}");
                Console.ResetColor();
                Console.ReadKey(true);
            }
        }
    }
}