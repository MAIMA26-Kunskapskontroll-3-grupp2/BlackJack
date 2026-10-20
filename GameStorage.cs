using System;
using System.Collections.Generic;
using System.IO;

public class GameStorage
{
    private string path;

    // TEMPORARY TEST: Verify that player results are saved between rounds.
    // Remove after code review.
    public void SavePlayers(List<Player> players)
    {
        File.WriteAllLines("players.txt",
            players.ConvertAll(player => player.Name));
    }

    public List<Player> LoadPlayers()
    {
        List<Player> players = new();

        if (File.Exists("players.txt"))
        {
            foreach (string name in File.ReadAllLines("players.txt"))
            {
                players.Add(new Player(name));
            }
        }

        return players;
    }
    public GameStorage(string path = "results.txt")
    {
        this.path = path;
    }
    public void Save(Dictionary<Player, Outcome> results)
    {
        string[] existingLines = Load();
        int lastRound = 0;

        foreach (string line in existingLines)
        {
            if (line.StartsWith("Runda ") &&
                int.TryParse(line.Substring(6), out int number) &&
                number > lastRound)
            {
                lastRound = number;
            }
        }

        int nextRound = lastRound + 1;

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            if (existingLines.Length > 0 &&
                !string.IsNullOrWhiteSpace(existingLines[^1]))
            {
                writer.WriteLine();
            }

            writer.WriteLine($"Runda {nextRound}");
            writer.WriteLine("Spelare | Poäng | Resultat");

            foreach (var result in results)
            {
                int points = result.Key.Hand.CalculateValue();
                writer.WriteLine($"{result.Key.Name} | {points} | {result.Value}");
            }

            writer.WriteLine();
        }
    }

    public string[] Load()
    {
        if (!File.Exists(path))
        {
            return new string[0];
        }

        return File.ReadAllLines(path);
    }

    public bool Validate(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return true;

        if (line.StartsWith("Runda "))
            return int.TryParse(line.Substring(6), out int roundNumber)
                   && roundNumber > 0;

        if (line.StartsWith("Spelare |"))
            return true;

        string[] parts = line.Split('|');

        if (parts.Length != 3)
            return false;

        string name = parts[0].Trim();
        string pointsText = parts[1].Trim();
        string outcomeText = parts[2].Trim();

        bool validName = !string.IsNullOrWhiteSpace(name);

        bool validPoints = int.TryParse(pointsText, out int points)
                           && points >= 0;

        bool validOutcome = Enum.TryParse<Outcome>(
            outcomeText, out _);

        return validName && validPoints && validOutcome;
    }
}
