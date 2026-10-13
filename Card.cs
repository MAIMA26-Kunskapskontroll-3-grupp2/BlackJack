public class Card
{
    public string Rank { get; }
    public string SuitSymbol { get; }

    public Card(string rank, string suitSymbol)
    {
        Rank = rank;
        SuitSymbol = suitSymbol;
    }
}