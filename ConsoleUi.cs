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
            Console.ReadKey();

            // Check if the userinput is whitespace: enter/tab/space
            if (char.IsWhiteSpace(userInput))
            {
                Console.WriteLine("\nTom inmatning.");
                continue;
            }
            // If the userinput is in lowercase, convert it to uppercase
            else if (char.ToUpper(userInput) == 'K')
            {
                return Move.Hit;
            }
            else if (char.ToUpper(userInput) == 'S')
            {
                return Move.Stand;
            }
            else
            {
                Console.WriteLine("\nDet alternativet existerar inte.");
                continue;
            }
        }
    }

    public void ShowOutcome(Player player, Outcome outcome)
    {
        // Print out the final results (who won, lost or got a push)
    }
}