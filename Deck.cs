using System.Runtime.InteropServices;

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

        // "This method involves some unsafe code and should be used
        // cautiously. The CollectionsMarshal approach relies on the
        // internal implementation of List<T>, which might change in
        // future versions of .NET."
        // Reference: https://okyrylchuk.dev/blog/efficient-memory-management-with-spans-in-dotnet/
        // But we don't have to care about that, because this is a 2 week school project. :)
        _random.Shuffle<Card>(CollectionsMarshal.AsSpan(_cards));
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