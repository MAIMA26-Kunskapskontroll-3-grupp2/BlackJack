public class Game
{
    private const int _numberOfDecks = 6;
    private const int _minCutCard = 234;
    private const int _maxCutCard = 312;
    private List<Player> _players = new List<Player>();
    private GameStorage _storage = new GameStorage();
    private ConsoleUi _ui = new ConsoleUi();

    // set by ChooseHouseRules() before every new game
    // "null", they will get a value before they are used
    private HouseRules _rules = null!;
    private Dealer _dealer = null!;
    private Shoe _shoe = null!;

    public void Run()
    {
        _storage.Load();
        _players = _storage.LoadPlayers();

        // Start MainMenu()
        while (true)
        {
            switch (_ui.MainMenu())
            {
                case 1:
                    AddPlayer();
                    break;
                case 2:
                    RemovePlayer();
                    break;
                case 3:
                    ShowStatistics();
                    break;
                case 4:
                    PlayGame();
                    break;
                case 5:
                    return;
            }
        }
    }

    private void ShowStatistics()
    {
        string[] statistics = _storage.LoadStatistics();

        if (statistics.Length == 0)
        {
            _ui.ShowMessage("Det finns ingen spelarstatistik att visa.");
            return;
        }

        _ui.ShowStatistics(statistics);
    }


    private void PlayGame()
    {
        if (_players.Count < 1)
        {
            _ui.ShowMessage("Det finns inga spelare. Var vänlig och lägg till minst en spelare.");
            return;
        }

        ChooseHouseRules();
        _dealer = new Dealer(_rules);
        _shoe = CreateShoe();

        bool keepPlaying = true;

        while (keepPlaying)
        {
            PlayRound();

            while (true)
            {
                string? answer = _ui.GetString("Vill ni spela en till runda (Ja/Nej): ");
                if (answer == null)
                    continue;

                char c = answer.ToUpper()[0];
                if (c == 'N')
                {
                    keepPlaying = false;
                    break;
                }

                if (c == 'J')
                    break;

                _ui.ShowMessage("Ogiltigt svar. Svara Ja eller Nej.");
            }
        }
    }

    // Let the players choose which house rules to play with
    private void ChooseHouseRules()
    {
        List<string> ruleSetNames = HouseRules.GetRuleSetNames();

        // If the rule file is missing or empty, HouseRules uses the standard rules
        if (ruleSetNames.Count == 0)
        {
            _rules = new HouseRules();
            return;
        }

        int index = _ui.RuleSetMenu(ruleSetNames.ToArray());
        _rules = new HouseRules(ruleSetNames[index]);
    }

    // Create a new shuffled shoe with the number of decks and cut card from the house rules
    private Shoe CreateShoe()
    {
        return new Shoe(_rules.NumberOfDecks, _rules.MinCutCard, _rules.MaxCutCard);
    }

    private void PlayRound()
    {
        // Check the shoe, create a Round object and execute the round's phases
        if (_shoe.IsTimeToShuffle)
        {
            _ui.ShowMessage($"Hittade skiljekortet. Blandar skon.");
            _shoe = new(_numberOfDecks, _minCutCard, _maxCutCard);
            CardCounter.Reset();
        }

        Round round = new Round(_shoe, _dealer, _players, _ui, _storage, _rules);
        round.InitialDeal();
        round.PlayPlayersTurns();
        round.PlayDealerTurn();
        round.FinishRound();
    }

    private void AddPlayer()
    {
        if (_players.Count >= 5)
        {
            _ui.ShowMessage("Spelet är fullt. Det finns redan 5 spelare.");
            return;
        }

        string? name = _ui.GetString("Ange spelarens namn: ");

        if (name != null)
        {
            _players.Add(new Player(name));
            _storage.SavePlayers(_players);
        }
        else
        {
            _ui.ShowMessage("Spelaren måste ha ett namn.");
        }

        ShowPlayers();

    }


    private void RemovePlayer()
    {
        if (_players.Count <= 0)
        {
            _ui.ShowMessage("Det finns inga spelare att ta bort.");
            return;
        }

        int index = _ui.RemovePlayerMenu(_players.Select(p => p.Name).ToArray());

        if (index < 0)
            return;

        if (index == _players.Count)
            return;


        _players.RemoveAt(index);
        _storage.SavePlayers(_players);
        ShowPlayers();

    }

    private void ShowPlayers()
    {
        if (_players.Count <= 0)
        {
            _ui.ShowMessage("Det finns ingen Spelarstatistik att visa.");
            return;
        }

        int i = 0;
        _ui.ShowList("Spelare:", _players.Select(p => $"{++i}. {p.Name}").ToArray());
    }
}
