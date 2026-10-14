public class Round
{
    private Deck _deck;
    private Dealer _dealer;
    private List<Player> _players;

    public Round(Deck deck, Dealer dealer, List<Player> players)
    {
        _deck = deck;
        _dealer = dealer;
        _players = players;
    }

    public void InitialDeal()
    {
        // Deal the starting cards according to the rules
        // (Players get two open cards, Dealer gets one open and one hidden hole card)
    }

    public void PlayPlayersTurns(ConsoleUi ui)
    {
        // Loop through the players and ask for their choices through the ui (hit/stand)
        foreach (Player player in _players)
        {
            bool playerTurnActive = true;

            while (playerTurnActive)
            {
                // Show the players' current cards on the screen
                // One of the dealer's card is still hidden, therefore the argument is false 
                ui.ShowHands(_dealer,_players, false);

                // Ask the player for their choice of move - hit/stand
                Move choice =  ui.AskForMove(player);

                if(choice == Move.Hit)
                {
                    // Player gets another card 
                    player.AddCard(_deck.DealCard());
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
        // Execute the dealer's hand based on ShouldHit()
    }

    public Dictionary<Player, Outcome> DetermineOutcomes()
    {
        // Compare the players' hands vs the dealer's and update the results
        // (Handles busts, highest hands, pushes and natural 21 wins)
        return new Dictionary<Player, Outcome>();
    }
}