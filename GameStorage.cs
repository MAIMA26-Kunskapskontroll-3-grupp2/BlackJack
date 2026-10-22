using System;
using System.Collections.Generic;
using System.IO;

public class GameStorage
{
    private string path;
    public List<Player> LoadPlayers()
    {
        List<Player> players = new();

        foreach (string line in Load())
        {
            string[] parts = line.Split('|');

            if (parts.Length != 3)
                continue;

            string name = parts[0].Trim();
            string outcome = parts[2].Trim();

            if (string.IsNullOrWhiteSpace(name))
                continue;

            if (!Enum.TryParse<Outcome>(outcome, out _))
                continue;

            if (!players.Exists(player => player.Name == name))
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

        int statisticsIndex = Array.FindIndex(
            existingLines, line => line.Trim() == "STATISTIK");

        if (statisticsIndex >= 0)
        {
            existingLines = existingLines.Take(statisticsIndex).ToArray();
            File.WriteAllLines(path, existingLines);
        }

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

        UpdateStatistics();
    }

    public string[] Load()
    {
        if (!File.Exists(path))
        {
            return Array.Empty<string>();
        }

        try
        {
            return File.ReadAllLines(path);
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte läsa spelfilen.");
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du har inte behörighet att läsa spelfilen.");
            throw;
        }
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
    public void UpdateStatistics()
    {
        string[] lines = Load();
        List<string> history = new List<string>();
        bool inStatistics = false;

        foreach (string line in lines)
        {
            if (line.Trim() == "STATISTIK")
            {
                inStatistics = true;
                continue;
            }

            if (!inStatistics)
            {
                history.Add(line);
            }
        }

        Dictionary<string, int[]> stats = new Dictionary<string, int[]>();

        foreach (string line in history)
        {
            if (line.StartsWith("Runda ") ||
                line.StartsWith("Spelare |") ||
                string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            if (parts.Length != 3)
                continue;

            string name = parts[0].Trim();
            string outcome = parts[2].Trim();

            if (!stats.ContainsKey(name))
                stats[name] = new int[5];

            if (outcome == "Win")
                stats[name][0]++;
            else if (outcome == "Lose")
                stats[name][1]++;
            else if (outcome == "Push")
                stats[name][2]++;
            else if (outcome == "BlackJack")
            {
                stats[name][0]++;
                stats[name][3]++;
            }

            stats[name][4]++;
        }

        List<string> output = new List<string>(history)
    {
        "",
        "STATISTIK",
        $"{ "Spelare",-15} { "Vinster",8} { "Förluster",10} { "Push",6} { "Blackjack",10} { "Rundor",7}",
        new string('-', 62)
    };

        foreach (var player in stats)
        {
            output.Add(
                $"{player.Key,-15} {player.Value[0],8} {player.Value[1],10} " +
                $"{player.Value[2],6} {player.Value[3],10} {player.Value[4],7}");
        }

        File.WriteAllLines(path, output);
    }
    public string[] LoadStatistics()
    {
        string[] lines = Load();
        int index = Array.FindIndex(lines, line => line.Trim() == "STATISTIK");


        if (index >= 0)
        {
            return lines.Skip(index).ToArray();
        }

        return Array.Empty<string>();
    }
}
