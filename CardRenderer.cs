public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string rank = GetRankSymbol(card.Rank);
        string suit = GetSuitSymbol(card.Suit);

        return new string[]
        {
            "┌───────┐",
            $"│{rank,-2}     │",
            $"│   {suit}   │",
            $"│     {rank,2}│",
            "└───────┘"
        };
    }

    private string GetRankSymbol(Rank rank)
    {
        if (rank <= Rank.Ten)
        {
            return ((int)rank).ToString();
        }

        switch (rank)
        {
            case Rank.Jack:
                return "J";
            case Rank.Queen:
                return "Q";
            case Rank.King:
                return "K";
            case Rank.Ace:
                return "A";
            default:
                return "?";
        }
    }
    private string GetSuitSymbol(Suit suit)
    {
        switch (suit)
        {
            case Suit.Hearts:
                return "♥";
            case Suit.Diamonds:
                return "♦";
            case Suit.Clubs:
                return "♣";
            case Suit.Spades:
                return "♠";
            default:
                return "?";
        }
    }
}