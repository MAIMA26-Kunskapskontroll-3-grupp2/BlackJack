public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string top = "┌───────┐";
        string rankTop = $"│{card.RankSymbol,-2}     │";
        string suitMiddle = $"│   {card.SuitSymbol}   │";
        string rankBottom = $"│     {card.RankSymbol,-2}│";
        string bottom = "└───────┘";

        return new string[] { top, rankTop, suitMiddle, rankBottom, bottom };
    }
}