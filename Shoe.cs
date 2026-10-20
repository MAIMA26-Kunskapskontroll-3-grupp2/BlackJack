public class Shoe
{
    private List<Card> _cards = new List<Card>();
    private int _numberOfDecks = 1;
    private static Random _random = new Random();

    public Shoe(int numberOfDecks)
    {
        // Builds and shuffles the shoe at the start of the game
        _numberOfDecks = numberOfDecks;

        BuildShoe();
        Shuffle();
    }

    private List<Card> CreateDeck()
    {
        List<Card> deck = [];

        foreach (Suit suit in typeof(Suit).GetEnumValues())
        {
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

    // Throws DeckIsEmptyException
    public Card DealCard()
    {
        // Deals out a card and knows when the deck has reached the minimum amount of cards left
        if (_cards.Count <= 0)
            throw new DeckIsEmptyException();

        Card card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public int CardsLeft()
    {
        // The amount of cards left
        return _cards.Count;
    }
}