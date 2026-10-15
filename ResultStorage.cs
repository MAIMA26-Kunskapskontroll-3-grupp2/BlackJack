using System;
using System.IO;

public static class ResultStorage
{
    // Path to the file where all round results will be stored.
    // The file is created automatically if it does not exist.
    private static string path = "results.txt";

    // Saves one round result to the file.
    // A timestamp is added so it is easy to see when the round was played.
    public static void Save(string result)
    {
        // Create one line with date + result text.
        string line = $"{DateTime.Now}: {result}";

        // AppendAllText means "add this line to the end of the file".
        // If the file does not exist, it will be created automatically.
        File.AppendAllText(path, line + Environment.NewLine);
    }

    // Loads all saved results when the game starts.
    // If the file does not exist, an empty array is returned.
    public static string[] Load()
    {
        // Check if the file exists. If not → return empty array.
        if (!File.Exists(path))
            return new string[0];

        // Read all lines and return them.
        return File.ReadAllLines(path);
    }

    // Validates that one line in the file has the correct format.
    // For now we only check that the line contains ":".
    public static bool Validate(string line)
    {
        // If the line contains ":" we consider it valid.
        return line.Contains(":");
    }
}