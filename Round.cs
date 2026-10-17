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
        // (Players get two open cards, Dealer gets one open and one hidden hole card)
        foreach (Player player in _players)
        {
            player.AddCard(_deck.DealCard());
            player.AddCard(_deck.DealCard());
        }

        _dealer.AddCard(_deck.DealCard());
        _dealer.AddCard(_deck.DealCard());

        _ui.ShowHands(_dealer, _players, false);
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
                    if(player.Hand.IsBust())
                    {
                        // Display 'you're busted' message 
                        _ui.ShowBustMessage(player);
                        // End the player turn
                        playerTurnActive = false;
                    }
                }
                else if(choice == Move.Stand)
                {
                    // The player wants to stop - end their turn
                    playerTurnActive = false;
                }
            }
        }
    }

    public void PlayDealerTurn()
    {
       while (_dealer.ShouldHit())
        {
            _dealer.AddCard(_deck.DealCard());
        }
    }

 public Dictionary<Player, Outcome> DetermineOutcomes()
{
    var results = new Dictionary<Player, Outcome>();

   int dealerValue = _dealer.Hand.CalculateValue();

    foreach (var player in _players)
    {
        int playerValue = player.Hand.CalculateValue();

        if (player.Hand.IsNatural21())
{
    results[player] = Outcome.BlackJack;
    continue;
}
               
         if (playerValue > 21)
        {
            results[player] = Outcome.Lose;
        }
        else if (playerValue < dealerValue)
        {
            results[player] = Outcome.Lose;
            }
        else if (playerValue > dealerValue)
        {
            results[player] = Outcome.Win;
        }
        else if (playerValue < dealerValue)
        {
            results[player] = Outcome.Lose;
        }
        else
        {
            results[player] = Outcome.Push;
        }
    }

    return results;
}
public void FinishRound()
{
    Dictionary<Player, Outcome> results = DetermineOutcomes();

    _storage.Save(results);
}
}