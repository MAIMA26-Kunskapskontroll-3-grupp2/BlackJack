using System;
using System.Collections.Generic;
using System.IO;

public class GameStorage
{
     private string path = "results.txt";
    public void Save(Dictionary<Player, Outcome> results, int roundNumber)
    {
         using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"Runda {roundNumber}");

            foreach (var result in results)
            {
                writer.WriteLine($"{result.Key.Name}: {result.Value}");
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
        return line.StartsWith("Runda ") || line.Contains(":");
    }
}