using System;
namespace GuessTheNumber2

{
    class Program
    {
        static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = 450;
            }
            GameConfiguration config = new GameConfiguration();
            HallOfFame hof = new HallOfFame();
            MenuController menu = new MenuController(config, hof);

            menu.MainLoop();
        }
    }
}