public class Round
{
    private Deck _deck;
    private Dealer _dealer;
    private List<Player> _players;
    private ConsoleUi _ui;


    public Round(Deck deck, Dealer dealer, List<Player> players, ConsoleUi ui)
    {
        _deck = deck;
        _dealer = dealer;
        _players = players;
        _ui = ui;
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
        _ui.ShowHands(_dealer, _players, false);
    }

    public void PlayPlayersTurns()
    {
        // Loop through the players and ask for their choices through the ui (hit/stand)
    }

    public void PlayDealerTurn()
    {
        // Execute the dealer's hand based on ShouldHit()
    }

    public Dictionary<Player, Outcome> DetermineOutcomes()
    {
        // Compare the players' hands vs the dealer's and update the results
        // (Handles busts, highest hands, pushes and natural 21 wins)
        return new Dictionary<Player, Outcome>();
    }
}