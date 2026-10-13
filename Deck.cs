public class Deck
{
    private List<Card> _cards = new List<Card>();
    private static Random _random = new Random();

    public Deck()
    {
        // Builds and shuffles the deck at the start of the game
    }

    public void Shuffle()
    {
        // Shuffle - Fisher-Yate's method
    }

    public Card DealCard()
    {
        // Deals out a card and knows when the deck has reached the minimum amount of cards left
        // null here is just a placeholder for a real card that should be returned later
        return null;
    }

    public int CardsLeft()
    {
        // The amount of cards left
        return _cards.Count;
    }
}