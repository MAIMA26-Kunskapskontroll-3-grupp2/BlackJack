public class ConsoleUi
{
    public void ShowMenu()
    {
        // Print the main menu
    }

    public void ShowHands(Dealer dealer, List<Player> players, bool revealDealer)
    {
        // Print the current cards and values of all contestans on the screen as text
    }

    public Move AskForMove(Player player)
    {
        // Ask the player if they want to hit or stand
        while (true)
        {
            Console.WriteLine($"\n{player.Name} tryck på 'K' för att köpa eller 'S' för att stanna");
            // Read userinput
            // Used char instead of string since we're only reading one character
            char userInput = Console.ReadKey().KeyChar;

            // Press any key to confirm the choice above
            // Setting it to "true" here prevents the second character from being printed to the screen
            Console.ReadKey(true);

            // Check if the userinput is whitespace: enter/tab/space
            if (char.IsWhiteSpace(userInput))
            {
                Console.WriteLine("\nTom inmatning.");
                continue;
            }
            // If the userinput is in lowercase, convert it to uppercase
            if (char.ToUpper(userInput) == 'K')
            {
                return Move.Hit;
            }

            if (char.ToUpper(userInput) == 'S')
            {
                return Move.Stand;
            }
            Console.WriteLine("\nDet alternativet existerar inte.");
        }
    }

    public void ShowOutcome(Player player, Outcome outcome)
    {
        // Print out the final results (who won, lost or got a push)
    }

    public void ShowBustMessage(Player player)
    {
        Console.WriteLine( $"\n{player.Name} du har spräckt!");
        Console.WriteLine( $"Du fick {player.Hand.CalculateValue()} poäng och har förlorat omedelbart.");
    }
}