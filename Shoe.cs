public class Shoe
{
    private List<Card> _cards = new List<Card>();
    private int _numberOfDecks;
    private int _minCutCard;
    private int _maxCutCard;
    private static Random _random = new Random();
    private bool _reShuffle = false;

    public bool IsTimeToShuffle { get { return _reShuffle; } }

    // Builds and shuffles the shoe at the start of the game.
    // Checks the values before using them.
    // _maxCutCard and _minCutCard are in %.
    public Shoe(int numberOfDecks, int minCutCard, int maxCutCard)
    {
        // Check the parameters before accepting them.
        _numberOfDecks = numberOfDecks > 0 ? numberOfDecks : 1;

        if (maxCutCard < 0)
            _maxCutCard = 0;
        else if (maxCutCard > 100)
            _maxCutCard = 100;
        else
            _maxCutCard = maxCutCard;

        if (minCutCard < 0)
            _minCutCard = 0;
        else if (minCutCard > _maxCutCard)
            _minCutCard = _maxCutCard;
        else
            _minCutCard = minCutCard;

        BuildShoe();
        Shuffle();
        InsertCutCard();
    }

    private List<Card> CreateDeck()
    {
        List<Card> deck = [];

        foreach (Suit suit in typeof(Suit).GetEnumValues())
        {
            if (suit == Suit.CutCard)
                continue;

            foreach (Rank rank in typeof(Rank).GetEnumValues())
                deck.Add(new(suit, rank));
        }

        return deck;
    }

    private void BuildShoe()
    {
        for (int i = 0; i < _numberOfDecks; i++)
            _cards.AddRange(CreateDeck());
    }

    private void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int r = _random.Next(i + 1);

            if (r == i)
                continue;

            // Shorthand for the standard 3 row swap.
            (_cards[r], _cards[i]) = (_cards[i], _cards[r]);
        }
    }

    private void InsertCutCard()
    {
        int index = _random.Next(
            _cards.Count - _maxCutCard * _cards.Count / 100,
            _cards.Count - _minCutCard * _cards.Count / 100);
        _cards.Insert(index, new(Suit.CutCard, 0));
    }

    // Throws ShoeIsEmptyException
    public Card DealCard()
    {
        Card card;

        // Deals out a card and knows when the shoe has reached the minimum amount of cards left
        while (true)
        {
            if (_cards.Count <= 0)
                throw new ShoeIsEmptyException();

            card = _cards[0];
            _cards.RemoveAt(0);

            if (card.Suit == Suit.CutCard)
            {
                _reShuffle = true;
                continue;
            }
            break;
        }

        return card;
    }

    public int RemainingDecks()
    {
        return _cards.Count / 52;
    }
}