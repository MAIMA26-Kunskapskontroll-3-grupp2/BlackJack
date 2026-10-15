public class Game
{
    private List<Player> _players = new List<Player>();
    private Dealer _dealer = new Dealer();
    private Deck _deck = new Deck();
    private GameStorage _storage = new GameStorage();
    private ConsoleUi _ui = new ConsoleUi();

    public void Start()
{
    Round round = new Round(_deck, _dealer, _players, _ui);

    round.InitialDeal();        // Funkar
    round.PlayPlayersTurns();   // Funkar

    Console.WriteLine("Dealer visar sina kort:");
    _ui.ShowHands(_dealer, _players, true);

    var results = round.DetermineOutcomes(); // Finns (tom)
    Console.WriteLine("Outcomes beräknade (placeholder).");
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