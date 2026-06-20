using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuessTheNumber2
{
    public class HallOfFame
    {
        private const string FilePath = "hall_of_fame.txt";
        private List<ScoreModel> _scores = new();

        public HallOfFame()
        {
            LoadScores();
        }

        public bool HasEntries => _scores.Count >0;

        public void AddScore(ScoreModel score)
        {
            _scores.Add(score);
            _scores.Sort();
            SaveScores();
        }

        public List<ScoreModel> GetTop5(string difficulty)
        {
            return _scores
                .Where(s => s.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                .Take(5)
                .ToList();
        }

        public void Clear()
        {
            _scores.Clear();
            if (File.Exists(FilePath)) File.Delete(FilePath);
        }

        private void SaveScores()
        {
            using StreamWriter writer = new StreamWriter(FilePath);
            foreach (var s in _scores)
            {
                writer.WriteLine($"{s.PlayerName};{s.Attempts};{s.DurationSeconds};{s.Difficulty};{s.IsNewGamePlus}");
            }
        }

        private void LoadScores()
        {
            if (!File.Exists(FilePath)) return;
            _scores.Clear();

            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 5)
                {
                    _scores.Add(new ScoreModel(
                        parts[0],
                        int.Parse(parts[1]),
                        int.Parse(parts[2]),
                        parts[3],
                        bool.Parse(parts[4])
                    ));
                }
            }
            _scores.Sort();
        }
    }
}