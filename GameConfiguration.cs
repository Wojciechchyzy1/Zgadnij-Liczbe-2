namespace GuessTheNumber2
{
    public class GameConfiguration
    {
        public bool AskForBet { get; set; } = true;
        // Język jest synchronizowany z LanguageManagerem
        public Language CurrentLanguage
        {
            get => LanguageManager.CurrentLanguage;
            set => LanguageManager.CurrentLanguage = value;
        }
    }
}