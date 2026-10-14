public class ConsoleUi
{
    public void ShowMenu()
    {
        // Print the main menu
    }

    // Print the current cards of the players and the dealer
    public void ShowHands(Dealer dealer, List<Player> players, bool revealDealer)
    {
        foreach (Player player in players)
        {
            ShowPlayerHand(player);
        }

        ShowDealerHand(dealer, revealDealer);
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

    private void ShowPlayerHand(Player player)
    {
        Console.Write($"{player.Name}: ");
        foreach (Card card in player.Hand.Cards)
        {
            ShowCard(card);
        }
        Console.WriteLine();
    }

    private void ShowDealerHand(Dealer dealer, bool revealDealer)
    {
        // Show the dealers hand
    }

    // Gets the symbol to print for the rank.
    private string GetRankSymbol(Rank rank)
    {
        if (rank <= Rank.Ten)
            return ((int)rank).ToString();

        switch (rank)
        {
            case Rank.Jack: return "J";
            case Rank.Queen: return "Q";
            case Rank.King: return "K";
            case Rank.Ace: return "A";
        }
        return "?";
    }

    // Gets the symbol to print for the suit.
    private string GetSuitSymbol(Suit suit)
    {
        switch (suit)
        {
            case Suit.Hearts: return "♥";
            case Suit.Diamonds: return "♦";
            case Suit.Clubs: return "♣";
            case Suit.Spades: return "♠";
        }
        return "?";
    }

    // Prints the symbol for the suit in appropriate color.
    private void ShowCard(Card card)
    {
        Console.ForegroundColor = card.Suit is Suit.Diamonds or Suit.Hearts
        ? ConsoleColor.Red : ConsoleColor.White;

        Console.Write(GetSuitSymbol(card.Suit));
        Console.ResetColor();
        Console.Write($"{GetRankSymbol(card.Rank)} ");
    }
}