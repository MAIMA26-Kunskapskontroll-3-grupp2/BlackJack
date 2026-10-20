public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string top = "┌───────┐";
        string rankTop = $"│{card.Rank,-2}     │";
        string suitMiddle = $"│   {GetSuitSymbol(card.Suit)}   │";
        string rankBottom = $"│     {card.Rank,-2}│";
        string bottom = "└───────┘";

        return new string[] { top, rankTop, suitMiddle, rankBottom, bottom };
    }
    private string GetSuitSymbol(Suit suit)
    {
        return suit switch
        {
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            Suit.Spades => "♠",
            _ => "?"
        };
    }
}