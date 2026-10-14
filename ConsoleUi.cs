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

    private void ShowPlayerHand(Player player)
    {
        Console.Write($"{player.Name}: ");
        foreach (Card card in player.Hand.Cards)
        {
            ShowCard(card);
        }
        Console.WriteLine($"poäng: {player.Hand.CalculateValue()}");
    }

    // Show the dealers hand
    private void ShowDealerHand(Dealer dealer, bool revealDealer)
    {
        Console.Write($"{dealer.Name}: ");
        for (int i = 0; i < dealer.Hand.Cards.Count; i++)
        {
            if (i == 1 && !revealDealer)
                ShowHiddenCard();
            else
                ShowCard(dealer.Hand.Cards[i]);
        }
        Console.WriteLine(revealDealer
            ? $"poäng: {dealer.Hand.CalculateValue()}"
            : "");
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

    // Prints the backside of the dealers hidden card.
    private void ShowHiddenCard()
    {
        Console.Write("▒▒ ");
    }
}