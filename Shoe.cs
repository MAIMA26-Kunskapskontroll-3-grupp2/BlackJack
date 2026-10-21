public class Shoe
{
    private List<Card> _cards = new List<Card>();
    private int _numberOfDecks;
    private static Random _random = new Random();
    private bool _reShuffle = false;

    public bool IsTimeToShuffle { get { return _reShuffle; } }

    public Shoe(int numberOfDecks)
    {
        // Builds and shuffles the shoe at the start of the game
        _numberOfDecks = numberOfDecks;

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
            {
                deck.Add(new(suit, rank));
            }
        }

        return deck;
    }

    private void BuildShoe()
    {
        for(int i = 0; i < _numberOfDecks; i++)
        {
            _cards.AddRange(CreateDeck());
        }
    }

    private void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int r = _random.Next(i + 1);

            if (r == i)
                continue;

            Card card = _cards[i];
            _cards[i] = _cards[r];
            _cards[r] = card;
        }
    }

    private void InsertCutCard()
    {
        int index = _random.Next(_cards.Count * 3 / 4, _cards.Count);
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
}