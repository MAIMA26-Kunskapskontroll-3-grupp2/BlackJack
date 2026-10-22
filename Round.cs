public class Round
{
    private Deck _deck;
    private Dealer _dealer;
    private List<Player> _players;
    private ConsoleUi _ui;
    private GameStorage _storage;

    public Round(Deck deck, Dealer dealer, List<Player> players, ConsoleUi ui, GameStorage storage)
    {
        _deck = deck;
        _dealer = dealer;
        _players = players;
        _ui = ui;
        _storage = storage;
    }

    // Deal the starting cards according to the rules
    public void InitialDeal()
    {
        //Remove dealer/player(s) cards from previous round
        _dealer.ClearHand();
        foreach (Player player in _players)
        {
            player.ClearHand();
        }

        // (Players get two open cards, Dealer gets one open and one hidden hole card)
        foreach (Player player in _players)
        {
            player.AddCard(_deck.DealCard());
            player.AddCard(_deck.DealCard());
        }

        _dealer.AddCard(_deck.DealCard());
        _dealer.AddCard(_deck.DealCard());
    }

    public void PlayPlayersTurns()
    {
        // Loop through the players and ask for their choices through the ui (hit/stand)
        foreach (Player player in _players)
        {
            bool playerTurnActive = true;

            while (playerTurnActive)
            {
                // Show the players' current cards on the screen
                // One of the dealer's card is still hidden, therefore the argument is false
                _ui.ShowHands(_dealer, _players, false);

                // Ask the player for their choice of move - hit/stand
                Move choice = _ui.AskForMove(player);

                if (choice == Move.Hit)
                {
                    // Player gets another card
                    player.AddCard(_deck.DealCard());

                    // Check if you're busted
                    if (player.Hand.IsBust())
                    {
                        _ui.ShowHands(_dealer, _players, false);
                        // Display 'you're busted' message
                        _ui.ShowBustMessage(player);
                        // End the player turn
                        playerTurnActive = false;
                    }
                }
                else if (choice == Move.Stand)
                {
                    // The player wants to stop - end their turn
                    playerTurnActive = false;
                }
            }
        }
    }

    public void PlayDealerTurn()
    {
        _ui.ShowMessage("Givaren vänder hålkortet.");
        while (_dealer.ShouldHit())
        {
            _dealer.AddCard(_deck.DealCard());
        }

        //Show all cards, including the dealer's hoel card
        _ui.ShowHands(_dealer, _players, true);

        if (_dealer.Hand.IsBust())
        {
            _ui.ShowMessage("Givaren har spruckit!");
        }
    }

    public Dictionary<Player, Outcome> DetermineOutcomes()
    {
        var results = new Dictionary<Player, Outcome>();

        //int dealerValue = _dealer.Hand.CalculateValue();

        foreach (var player in _players)
        {
            //int playerValue = player.Hand.CalculateValue();
            results[player] = GetOutcome(player);
        }
        return results;
    }

    private Outcome GetOutcome(Player player)
    {
        Hand playerHand = player.Hand;
        Hand dealerHand = _dealer.Hand;

        //A player that has busted always lose, even if the dealer also has busted
        if (playerHand.IsBust())
        {
            return Outcome.Lose;
        }

        //natural for dealer/player is a draw (push)
        if (playerHand.IsNatural21() && dealerHand.IsNatural21())
        {
            return Outcome.Push;
        }

        //natural 21 (only two cards), beats all hands (with more than 2 cards)
        if (playerHand.IsNatural21())
        {
            return Outcome.BlackJack;
        }

        if (dealerHand.IsNatural21())
        {
            return Outcome.Lose;
        }

        if (dealerHand.IsBust())
        {
            return Outcome.Win;
        }

        //none has busted, highest hand wins
        int playerValue = playerHand.CalculateValue();
        int dealerValue = dealerHand.CalculateValue();

        if (playerValue > dealerValue)
        {
            return Outcome.Win;
        }
        else if (playerValue < dealerValue)
        {
            return Outcome.Lose;
        }
        else
        {
            return Outcome.Push;
        }
    }

    public void FinishRound()
    {
        Dictionary<Player, Outcome> results = DetermineOutcomes();

        _ui.ShowMessage("Resultat:", false);
        foreach (Player player in _players)
        {
            Outcome outcome = results[player];
            player.AddResult(outcome);
            _ui.ShowOutcome(player, outcome);
        }
        _ui.NewLine();

        _storage.Save(results);
    }
}