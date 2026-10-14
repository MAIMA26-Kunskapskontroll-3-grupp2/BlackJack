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
    }

    private void MainMenu()
    {
        // Game-loop
    }

    private void PlayRound()
    {
        // Check the deck, create a Round object and execute the round's phases
        Round round = new(_deck, _dealer, _players, _ui);
        round.InitialDeal();
    }
}