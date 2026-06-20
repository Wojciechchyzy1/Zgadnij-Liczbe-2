using System;

namespace GuessTheNumber2
{
    public class ScoreModel : IComparable<ScoreModel>
    {
        public string PlayerName { get; set; }
        public int Attempts { get; set; }
        public int DurationSeconds { get; set; }
        public string Difficulty { get; set; }
        public bool IsNewGamePlus { get; set; }

        public ScoreModel(string playerName, int attempts, int durationSeconds, string difficulty, bool isNewGamePlus)
        {
            PlayerName = playerName;
            Attempts = attempts;
            DurationSeconds = durationSeconds;
            Difficulty = difficulty;
            IsNewGamePlus = isNewGamePlus;
        }

        //Najpierw mniejsza ilość prób. Jeśli równo,to mniejszy czas.
        public int CompareTo(ScoreModel? other)
        {
            if (other == null) return 1;
            int attemptCompare = this.Attempts.CompareTo(other.Attempts);
            if (attemptCompare != 0) return attemptCompare;
            return this.DurationSeconds.CompareTo(other.DurationSeconds);
        }

        public override string ToString()
        {
            string modeTag = IsNewGamePlus ? " [NG+]" : "";
            return $"{PlayerName,-15} | Próby: {Attempts,-3} | Czas: {DurationSeconds}s{modeTag}";
        }
    }
}