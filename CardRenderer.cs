public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string top = "┌───────┐";
        string rankTop = $"│{card.Rank,-2}     │";
        string suitMiddle = $"│   {card.SuitSymbol}   │";
        string rankBottom = $"│     {card.Rank,-2}│";
        string bottom = "└───────┘";

        return new string[] { top, rankTop, suitMiddle, rankBottom, bottom };
    }
}