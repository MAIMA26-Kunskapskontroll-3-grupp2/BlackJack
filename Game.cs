public class Game
{
    private List<Player> _players = new List<Player>();
    private Dealer _dealer = new Dealer();
    private Deck _deck = new Deck();
    private GameStorage _storage = new GameStorage();
    private ConsoleUi _ui = new ConsoleUi();

    public void Start()
{
    _players.AddRange(new List<Player>
    {
        new Player("Tomas"),
        new Player("Ai"),
        new Player("Gentiana"),
        new Player("Arne"),
        new Player("Benjamin")
    });

   // Läs in tidigare sparade resultat.
string[] savedResults = _storage.Load();

foreach (string line in savedResults)
{
    if (!_storage.Validate(line))
    {
        Console.WriteLine("Felaktigt format i results.txt.");
        return;
    }
}

PlayRound();
}
     private void MainMenu()
    {
        // Game-loop
    }

    private void PlayRound()
    {
        // Check the deck, create a Round object and execute the round's phases
         Round round = new Round(_deck, _dealer, _players, _ui, _storage);
         round.InitialDeal();
        round.PlayPlayersTurns();
        round.PlayDealerTurn();
        round.DetermineOutcomes();
        round.FinishRound();
    }
}