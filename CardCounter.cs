static class CardCounter
{
    public static int Count { get; private set; }

    // 2 - 6 count as low, 10 - Ace count as hi.
    public static void CountCard(Card card)
    {
        if (card.Rank <= Rank.Six)
            Count--;
        else if (card.Rank >= Rank.Ten)
            Count++;
    }

    public static void Reset()
    {
        Count = 0;
    }
}
