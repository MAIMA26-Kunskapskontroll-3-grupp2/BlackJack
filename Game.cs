public class Game
{
    private List<Player> _players = new List<Player>();
    private Dealer _dealer = new Dealer();
    private Deck _deck = new Deck();
    private GameStorage _storage = new GameStorage();
    private ConsoleUi _ui = new ConsoleUi();

    public void Start()
    {
        // Test code
        Player eddie = new Player("Eddie");
        ConsoleUi ui = new ConsoleUi();
        _players.Add(eddie);
        Round round = new Round(new Deck(), new Dealer(), _players);
        
        round.PlayPlayersTurns(ui);

        // Load players from _storage
        // Start MainMenu()
    }

    private void MainMenu()
    {
        // Game-loop
    }

    private void PlayRound()
    {
        // Check the deck, create a Round object and execute the round's phases
    }
}