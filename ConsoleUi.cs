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
        return Move.Stand; // Temporary placeholder
    }

    public void ShowOutcome(Player player, Outcome outcome)
    {
        // Print out the final results (who won, lost or got a push)
    }
}