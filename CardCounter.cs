static class CardCounter
{
    public static int RunningCount { get; private set; }

    // 2 - 6 count as low, 10 - Ace count as hi.
    public static Card CountCard(Card card)
    {
        if (card.Rank <= Rank.Six)
            RunningCount--;
        else if (card.Rank >= Rank.Ten)
            RunningCount++;

        return card;
    }

    public static void Reset()
    {
        RunningCount = 0;
    }
}
