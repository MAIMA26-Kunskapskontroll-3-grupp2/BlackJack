using System;
using System.Collections.Generic;
using System.IO;

public class GameStorage
{
    private string path;

    public GameStorage(string path = "results.txt")
    {
        this.path = path;
    }

    public void Save(Dictionary<Player, Outcome> results)
    {
        Dictionary<string, int[]> stats = new Dictionary<string, int[]>();

        foreach (string line in Load())
        {
            if (line.Trim() == "STATISTIK" ||
                line.StartsWith("Spelare") ||
                line.StartsWith("---") ||
                string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split(
                ' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 6)
                continue;

            string name = string.Join(" ", parts.Take(parts.Length - 5));

            int start = parts.Length - 5;

            if (int.TryParse(parts[start], out int wins) &&
                int.TryParse(parts[start + 1], out int losses) &&
                int.TryParse(parts[start + 2], out int pushes) &&
                int.TryParse(parts[start + 3], out int blackjacks) &&
                int.TryParse(parts[start + 4], out int rounds))
            {
                stats[name] = new int[]
                {
                wins, losses, pushes, blackjacks, rounds
                };
            }
        }

        foreach (var result in results)
        {
            string name = result.Key.Name;

            if (!stats.ContainsKey(name))
                stats[name] = new int[5];

            int[] playerStats = stats[name];

            switch (result.Value.ToString())
            {
                case "Win":
                    playerStats[0]++;
                    break;

                case "Lose":
                    playerStats[1]++;
                    break;

                case "Push":
                    playerStats[2]++;
                    break;

                case "BlackJack":
                    playerStats[0]++;
                    playerStats[3]++;
                    break;
            }

            playerStats[4]++;
        }

        List<string> output = new List<string>
    {
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

        try
        {
            File.WriteAllLines(path, output);
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte spara statistiken.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du saknar behörighet att spara statistiken.");
        }
    }

    public string[] Load()
    {
        try
        {
            if (!File.Exists(path))
            {
                return Array.Empty<string>();
            }

            return File.ReadAllLines(path);
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte läsa spelfilen.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du har inte behörighet att läsa spelfilen.");
        }

        return Array.Empty<string>();
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
    public List<Player> LoadPlayers()
    {
        List<Player> players = new List<Player>();

        try
        {
            if (!File.Exists("players.txt"))
                return players;

            foreach (string line in File.ReadAllLines("players.txt"))
            {
                string name = line.Trim();

                if (!string.IsNullOrWhiteSpace(name))
                    players.Add(new Player(name));
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte läsa spelarfilerna.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du saknar behörighet att läsa spelarfilerna.");
        }

        return players;
    }

    public void SavePlayers(List<Player> players)
    {
        try
        {
            File.WriteAllLines("players.txt", players.Select(player => player.Name));
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte spara spelarna.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du saknar behörighet att spara spelarna.");
        }
    }

}
