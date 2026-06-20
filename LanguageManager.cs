using System;
using System.Collections.Generic;

namespace GuessTheNumber2
{
    public enum Language { PL, EN }

    public static class LanguageManager
    {
        public static Language CurrentLanguage { get; set; } = Language.PL;

        private static readonly Dictionary<string, Dictionary<Language, string>> Phrases = new()
        {
            { "MenuTitle", new() { { Language.PL, "=== ZGADNIJ LICZBĘ 2! ===" }, { Language.EN, "=== GUESS THE NUMBER 2! ===" } } },
            { "NewGame", new() { { Language.PL, "1. Nowa Gra" }, { Language.EN, "1. New Game" } } },
            { "HallOfFame", new() { { Language.PL, "2. Hall of Fame" }, { Language.EN, "2. Hall of Fame" } } },
            { "Settings", new() { { Language.PL, "3. Ustawienia" }, { Language.EN, "3. Settings" } } },
            { "Exit", new() { { Language.PL, "0. Wyjście" }, { Language.EN, "0. Exit" } } },
            { "SelectDifficulty", new() { { Language.PL, "Wybierz poziom trudności:" }, { Language.EN, "Select difficulty level:" } } },
            { "Easy", new() { { Language.PL, "1. Łatwy (1-50)" }, { Language.EN, "1. Easy (1-50)" } } },
            { "Medium", new() { { Language.PL, "2. Średni (1-100)" }, { Language.EN, "2. Medium (1-100)" } } },
            { "Hard", new() { { Language.PL, "3. Trudny (1-250)" }, { Language.EN, "3. Hard (1-250)" } } },
            { "GameMode", new() { { Language.PL, "Wybierz tryb gry:" }, { Language.EN, "Select game mode:" } } },
            { "StandardGame", new() { { Language.PL, "1. Standardowa Gra" }, { Language.EN, "1. Standard Game" } } },
            { "NewGamePlus", new() { { Language.PL, "2. Nowa Gra Plus (Reroll liczby)" }, { Language.EN, "2. New Game Plus (Number reroll)" } } },
            { "BetPrompt", new() { { Language.PL, "Czy chcesz uruchomić tryb zakładu? (t/n):" }, { Language.EN, "Do you want to enable bet mode? (y/n):" } } },
            { "BetAttempts", new() { { Language.PL, "Podaj maksymalną liczbę prób:" }, { Language.EN, "Enter maximum number of attempts:" } } },
            { "Attempt", new() { { Language.PL, "Próba" }, { Language.EN, "Attempt" } } },
            { "EnterNumber", new() { { Language.PL, "Podaj liczbę: " }, { Language.EN, "Enter number: " } } },
            { "Win", new() { { Language.PL, "Gratulacje! Trafiłeś!" }, { Language.EN, "Congratulations! You guessed it!" } } },
            { "GameOver", new() { { Language.PL, "Koniec gry! Skończyły Ci się próby." }, { Language.EN, "Game Over! You ran out of attempts." } } },
            { "EnterName", new() { { Language.PL, "Podaj swoje imię: " }, { Language.EN, "Enter your name: " } } },
            { "Time", new() { { Language.PL, "Czas rozgrywki: {0}s" }, { Language.EN, "Game duration: {0}s" } } },
            { "ConfirmClear", new() { { Language.PL, "Czy na pewno chcesz wyczyścić Hall of Fame? (t/n):" }, { Language.EN, "Are you sure you want to clear Hall of Fame? (y/n):" } } }
        };

        public static string Get(string key)
        {
            if (Phrases.ContainsKey(key) && Phrases[key].ContainsKey(CurrentLanguage))
                return Phrases[key][CurrentLanguage];
            return key;
        }
    }
}