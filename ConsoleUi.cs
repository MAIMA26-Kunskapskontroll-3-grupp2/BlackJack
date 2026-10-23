public class ConsoleUi
{
    private readonly CardRenderer _cardRenderer = new();
    // Show the main menu and get a reply.
    public int MainMenu()
    {
        while (true)
        {
            // Console.WriteLine();
            Console.WriteLine("1. Lägg till spelare");
            Console.WriteLine("2. Ta bort spelare");
            Console.WriteLine("3. Visa Spelarstatistik");
            Console.WriteLine("4. Börja spela");
            Console.WriteLine("5. Avsluta spelet");
            Console.Write("Vad vill du göra: ");

            string? input = Console.ReadLine();
            if (input != null)
            {
                if (int.TryParse(input, out int i))
                {
                    if (1 <= i && i <= 5)
                    {
                        Console.WriteLine();
                        return i;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Ogiltigt val.");
            Console.WriteLine();
        }
    }

    // Show the remove player menu and get a reply.
    public int RemovePlayerMenu(string[] playerNames)
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {playerNames[i]}");
        }
        Console.WriteLine($"{playerNames.Length + 1}. Tillbaka till huvudmenyn");
        Console.Write("Vilken spelare vill du ta bort: ");

        string? input = Console.ReadLine();
        Console.WriteLine();
        if (input != null)
        {
            if (int.TryParse(input, out int i))
            {
                i--;
                if (i >= 0 && i <= playerNames.Length)
                    return i;
            }
        }
        Console.WriteLine("Ogiltigt val.");
        Console.WriteLine();
        return -1;
    }

    public void ShowList(string title, string[] lines)
    {
        Console.WriteLine(title);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine();
    }

    // Show the house rules menu and get a reply.
    public int RuleSetMenu(string[] ruleSetNames)
    {
        while (true)
        {
            Console.WriteLine();
            for (int i = 0; i < ruleSetNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {ruleSetNames[i]}");
            }
            Console.Write("Vilka husregler vill ni spela med: ");

            string? input = Console.ReadLine();
            if (input != null)
            {
                if (int.TryParse(input, out int i))
                {
                    i--;
                    if (i >= 0 && i < ruleSetNames.Length)
                        return i;
                }
            }
            Console.WriteLine("Ogiltigt val.");
        }
    }

    // Show a message and get a reply to it.
    public string? GetString(string message)
    {
        Console.Write(message);

        string? input = Console.ReadLine();
        Console.WriteLine();

        return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
    }

    // Show a message.
    public void ShowMessage(string message, bool extraNewLine = true)
    {
        Console.WriteLine(message);

        if (extraNewLine)
            Console.WriteLine();
    }

    public void ShowStatistics(string[] lines)
    {
        Console.WriteLine("=== SPELARSTATISTIK ===");
        Console.WriteLine();

        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();
        Console.WriteLine("Tryck på en tangent för att fortsätta...");
        Console.ReadKey(true);
    }


    public void NewLine()
    {
        Console.WriteLine();
    }

    // Show an error message
    public void ShowErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    // Show the current cards of the players and the dealer
    public void ShowHands(Dealer dealer, List<Player> players, bool revealDealer)
    {
        foreach (Player player in players)
        {
            ShowPlayerHand(player);
        }

        ShowDealerHand(dealer, revealDealer);
        Console.WriteLine();
    }

    public void ShowCardCount(int remainingDecks)
    {
        int trueCount = CardCounter.TrueCount(remainingDecks);
        Console.WriteLine($"Löpande räkning: {CardCounter.RunningCount}");
        Console.Write($"Sann räkning: {trueCount} ");

        if (trueCount > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('☀', trueCount));
        }
        else if (trueCount < 0)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('☁', -trueCount));
        }
        else
            Console.WriteLine();

        Console.ResetColor();
    }

    // Ask the player if they want to hit or stand
    public Move AskForMove(Player player)
    {
        while (true)
        {
            Console.Write($"{player.Name} tryck på 'K' för att köpa eller 'S' för att stanna: ");
            string? input = Console.ReadLine();
            Console.WriteLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.ToUpper().Trim();
                switch (input)
                {
                    case "K": return Move.Hit;
                    case "S": return Move.Stand;
                }
            }
            Console.WriteLine("Det alternativet existerar inte.");
            Console.WriteLine();
        }
    }

    public void ShowOutcome(Player player, Outcome outcome)
    {
        string text = "";
        switch (outcome)
        {
            case Outcome.Win:
                text = "Vinst";
                break;
            case Outcome.BlackJack:
                text = "Vinst med naturlig tjugoett!";
                break;
            case Outcome.Lose:
                text = "Förlust";
                break;
            case Outcome.Push:
                text = "Lika";
                break;
        }
        Console.WriteLine($"{player.Name} ({player.Hand.CalculateValue()} poäng): {text}");
    }

    public void ShowBustMessage(Player player)
    {
        Console.WriteLine();
        Console.WriteLine($"{player.Name} du har spräckt!");
        Console.WriteLine($"Du fick {player.Hand.CalculateValue()} poäng och har förlorat omedelbart.");
        Console.WriteLine();
    }

    // Show a players hand.
    private void ShowPlayerHand(Player player)
    {
        Console.WriteLine($"{player.Name}: Poäng {player.Hand.CalculateValue()}");
        ShowCards(player.Hand.Cards);

    }


    // Show the dealers hand.
    private void ShowDealerHand(Dealer dealer, bool revealDealer)
    {
        if (revealDealer)
        {
            Console.WriteLine($"{dealer.Name}: Poäng {dealer.Hand.CalculateValue()}");
        }
        else
        {
            Console.WriteLine($"{dealer.Name}:");
        }

        ShowCards(dealer.Hand.Cards, !revealDealer);
    }


    // Visar korten bredvid varandra.
    private void ShowCards(List<Card> cards, bool hideSecondCard = false)
    {
        if (cards.Count == 0)
        {
            return;
        }

        List<string[]> cardRows = new();

        for (int i = 0; i < cards.Count; i++)
        {
            if (hideSecondCard && i == 1)
            {
                cardRows.Add(new string[]
                {
                    "┌───────┐",
                    "│▒▒▒▒▒▒▒│",
                    "│▒▒▒▒▒▒▒│",
                    "│▒▒▒▒▒▒▒│",
                    "└───────┘"
                });
            }
            else
            {
                cardRows.Add(_cardRenderer.RenderCard(cards[i]));
            }
        }

        for (int row = 0; row < cardRows[0].Length; row++)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (hideSecondCard && i == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (cards[i].Suit == Suit.Hearts ||
                         cards[i].Suit == Suit.Diamonds)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(cardRows[i][row] + " ");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
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
}