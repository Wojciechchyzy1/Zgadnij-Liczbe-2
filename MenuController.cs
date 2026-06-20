using System;
using System.Collections.Generic;

namespace GuessTheNumber2
{
    public class MenuController
    {
        private readonly GameConfiguration _config;
        private readonly HallOfFame _hof;
        private readonly GameEngine _engine;

        public MenuController(GameConfiguration config, HallOfFame hof)
        {
            _config = config;
            _hof = hof;
            _engine = new GameEngine(_config, _hof);
        }

        public void MainLoop()
        {
            int activeOption = 0;

            while (true)
            {
                
                List<string> options = new List<string> { LanguageManager.Get("NewGame") };
                if (_hof.HasEntries) options.Add(LanguageManager.Get("HallOfFame"));
                options.Add(LanguageManager.Get("Settings"));
                options.Add(LanguageManager.Get("Exit"));

                if (activeOption >= options.Count) activeOption = 0;

                Console.Clear();
                RenderAsciiArt();
                Console.WriteLine($"\n   {LanguageManager.Get("MenuTitle")}\n");

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == activeOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($" >  {options[i]}  <");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {options[i]}");
                    }
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                PlayBeep(440, 50);

                if (key == ConsoleKey.UpArrow)
                {
                    activeOption = (activeOption - 1 + options.Count) % options.Count;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    activeOption = (activeOption + 1) % options.Count;
                }
                else if (key == ConsoleKey.Enter)
                {
                    PlayBeep(600, 100); 
                   
                    string selectedText = options[activeOption];

                    if (selectedText == LanguageManager.Get("NewGame")) _engine.StartNewGame();
                    else if (selectedText == LanguageManager.Get("HallOfFame")) ShowHallOfFameMenu();
                    else if (selectedText == LanguageManager.Get("Settings")) ShowSettingsMenu();
                    else if (selectedText == LanguageManager.Get("Exit")) break;
                }
            }
        }

        private void ShowSettingsMenu()
        {
            int activeOption = 0;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ThemeManager.Primary;
                Console.WriteLine("=== USTAWIENIA / SETTINGS ===\n");
                Console.ResetColor();

                
                string[] options = {
            $"[1] Język / Language: {_config.CurrentLanguage}",
            $"[2] Pytaj o zakład / Ask for Bet: {(_config.AskForBet ? "TAK/YES" : "NIE/NO")}",
            $"[3] Motyw / Theme: {ThemeManager.CurrentTheme}",
            $"[4] Wyczyść / Clear Hall of Fame",
            $"[0] Powrót / Back"
        };

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == activeOption)
                    {
                        Console.ForegroundColor = ThemeManager.Accent;
                        Console.WriteLine($" -> [ {options[i]} ]");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {options[i]}");
                    }
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                PlayBeep(440, 50);

                if (key == ConsoleKey.UpArrow)
                {
                    activeOption = (activeOption - 1 + options.Length) % options.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    activeOption = (activeOption + 1) % options.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    
                    if (activeOption == 0)
                    {
                        _config.CurrentLanguage = _config.CurrentLanguage == Language.PL ? Language.EN : Language.PL;
                    }
                    
                    else if (activeOption == 1)
                    {
                        _config.AskForBet = !_config.AskForBet;
                    }
                    
                    else if (activeOption == 2)
                    {
                        ThemeManager.CurrentTheme = ThemeManager.CurrentTheme switch
                        {
                            GameTheme.Cyberpunk => GameTheme.Classic,
                            GameTheme.Classic => GameTheme.Hacker,
                            _ => GameTheme.Cyberpunk
                        };
                    }
                    
                    else if (activeOption == 3)
                    {
                        Console.Write($"\n{LanguageManager.Get("ConfirmClear")} ");
                        string confirm = Console.ReadLine()?.ToLower();
                        if (confirm == "t" || confirm == "y")
                        {
                            _hof.Clear();
                            Console.WriteLine(LanguageManager.CurrentLanguage == Language.PL ? "Wyczyszczono!" : "Cleared!");
                            PlayBeep(300, 300);
                            Console.ReadKey(true);
                        }
                    }
                    
                    else if (activeOption == 4)
                    {
                        break;
                    }
                }
            }
        }

        private void ShowHallOfFameMenu()
        {
            string[] difficulties = { "Easy", "Medium", "Hard" };
            int currentDiffIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"=== Hall of Fame: {difficulties[currentDiffIndex].ToUpper()} ===");
                Console.ResetColor();
                Console.WriteLine(LanguageManager.CurrentLanguage == Language.PL
                    ? " [ ◄ / ► Strzałki: Zmiana poziomu ]  [ ESC: Powrót ]\n"
                    : " [ ◄ / ► Arrows: Switch Difficulty ]  [ ESC: Return ]\n");

                List<ScoreModel> top5 = _hof.GetTop5(difficulties[currentDiffIndex]);

                if (top5.Count == 0)
                {
                    Console.WriteLine("   Brak wpisów / No entries");
                }
                else
                {
                    for (int i = 0; i < top5.Count; i++)
                    {
                        if (top5[i].IsNewGamePlus)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta; 
                            Console.WriteLine($" 🏆 {i + 1}. {top5[i]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"    {i + 1}. {top5[i]}");
                        }
                    }
                }

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow) currentDiffIndex = (currentDiffIndex - 1 + 3) % 3;
                else if (key == ConsoleKey.RightArrow) currentDiffIndex = (currentDiffIndex + 1) % 3;
                else if (key == ConsoleKey.Escape) break;
            }
        }

        private void RenderAsciiArt()
        {
            Console.ForegroundColor = ThemeManager.Primary;
            Console.WriteLine(@"  ________                               __                       ________ ");
            Console.WriteLine(@" /  _____/ __ __   ____   ______ ______ |__| ____   ____         \_____  \");
            Console.WriteLine(@"/   \  ___|  |  \_/ __ \ /  ___//  ___/ |  |/    \ /  _ \         /  ____/");
            Console.WriteLine(@"\    \_\  \  |  /\  ___/ \___ \ \___ \  |  |   |  (  <_> )       /       \ ");
            Console.WriteLine(@" \______  /____/  \___  >____  >____  > |__|___|  /\____/  /\    \_______ \");
            Console.WriteLine(@"        \/            \/     \/     \/          \/         \/            \/");
            Console.ResetColor();
        }

        private void PlayBeep(int freq, int duration)
        {
            try { Console.Beep(freq, duration); } catch { }
        }
    }
}