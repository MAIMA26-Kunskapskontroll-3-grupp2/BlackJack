public class Game
{
    private List<Player> _players = new List<Player>();
    private Dealer _dealer = new Dealer();
    private Deck _deck = new Deck();
    private GameStorage _storage = new GameStorage();
    private ConsoleUi _ui = new ConsoleUi();

    public void Start()
    {
        // Load players from _storage
        // Start MainMenu()
        _players.AddRange(
            new Player("Tomas"),
            new Player("Ai"),
            new Player("Gentiana"),
            new Player("Arne"),
            new Player("Benjamin")
        );
        PlayRound();
    }

        round.InitialDeal();
        round.PlayPlayersTurns();
        round.PlayDealerTurn();
        round.DetermineOutcomes();
        round.FinishRound();

        // Visa meny, fråga om ny runda osv
    }
     private void MainMenu()
    {
        // Game-loop
    }

    private void PlayRound()
    {
        // Check the deck, create a Round object and execute the round's phases
        Round round = new(_deck, _dealer, _players, _ui);

        // Test code, to be replaced with proper game code
        Console.WriteLine();
        Console.WriteLine("Initial deal:");
        round.InitialDeal();
        foreach (Player player in _players)
        {
            while (player.Hand.CalculateValue() < 17)
                player.AddCard(_deck.DealCard());
        }

        Console.WriteLine();
        Console.WriteLine("End of automated test game:");
        while (_dealer.Hand.CalculateValue() < 17)
            _dealer.AddCard(_deck.DealCard());
        _ui.ShowHands(_dealer, _players, true);
    }
}