public class Deck
{
    private List<Card> _cards = new List<Card>();
    private static Random _random = new Random();

    public Deck()
    {
        // Builds and shuffles the deck at the start of the game
        foreach (Suit suit in typeof(Suit).GetEnumValues())
        {
            foreach (Rank rank in typeof(Rank).GetEnumValues())
            {
                _cards.Add(new(suit, rank));
            }
        }

        Shuffle();
    }

    public void Shuffle()
    {
        // Shuffle - Fisher-Yate's method
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int r = _random.Next(i);

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