using System;

namespace GuessTheNumber2
{
    public enum GameTheme { Cyberpunk, Classic, Hacker }

    public static class ThemeManager
    {
        public static GameTheme CurrentTheme { get; set; } = GameTheme.Cyberpunk;

        public static ConsoleColor Primary => CurrentTheme switch
        {
            GameTheme.Classic => ConsoleColor.Green,
            GameTheme.Hacker => ConsoleColor.DarkGreen,
            _ => ConsoleColor.DarkCyan // Cyberpunk
        };

        public static ConsoleColor Accent => CurrentTheme switch
        {
            GameTheme.Classic => ConsoleColor.White,
            GameTheme.Hacker => ConsoleColor.Green,
            _ => ConsoleColor.Magenta // Cyberpunk
        };

        public static ConsoleColor Warning => ConsoleColor.Red;
    }
}