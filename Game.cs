public class Game
{
    private List<Player> _players = new List<Player>();
    private Dealer _dealer = new Dealer();
    private Deck _deck = new Deck();
    private GameStorage _storage = new GameStorage();
    private ConsoleUi _ui = new ConsoleUi();

    public void Run()
    {
        // Läs in tidigare sparade resultat.
        string[] savedResults = _storage.Load();

        foreach (string line in savedResults.TakeWhile(line => line.Trim() != "STATISTIK"))
        {
            if (!_storage.Validate(line))
            {
                _ui.ShowMessage("Felaktigt format i results.txt.");
                return;
            }
        }

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

    private void PlayRound()
    {
        // Check the deck, create a Round object and execute the round's phases
        if (_deck.CardsLeft() <= 15)
        {
            _ui.ShowMessage($"Det finns bara {_deck.CardsLeft()} kort kvar i leken. Blandar kortleken.");
            _deck = new();
        }

        Round round = new Round(_deck, _dealer, _players, _ui, _storage);
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

        string? name = _ui.GetString($"Ange spelarens namn: ");

        if (name != null)
        {
            _players.Add(new(name));
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