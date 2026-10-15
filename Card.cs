public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }
    public bool IsFaceUp { get; set; } // Controls visibility
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
}