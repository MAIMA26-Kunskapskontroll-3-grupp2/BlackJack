public class Hand
{
    // Private list so only this class can make changes to it
    private List<Card> _cards = new List<Card>();
    // Public list so others classes can look at the cards but not make any changes to them
    public List<Card> Cards { get { return _cards; } }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void Clear()
    {
        _cards.Clear();
    }

    // Calculate the sum value and handle soft/hard aces
    public int CalculateValue()
    {
        int score = 0;
        int aces = 0;

        foreach (Card card in _cards)
        {
            if (card.Rank == Rank.Ace)
            {
                score += 11;
                aces++;
            }
            else if (card.Rank < Rank.Jack)
                score += (int)card.Rank;
            else
                score += 10;
        }

        while (aces > 0 && score > 21)
        {
            score -= 10;
            aces--;
        }

        return score;
    }

    // Check if the hand's total value is over 21
    public bool IsBust()
    {
        return CalculateValue() > 21;
    }

    // Check if the player got exactly 21 points on their first two cards
    public bool IsNatural21()
    {
        return Cards.Count == 2 && CalculateValue() == 21;
    }
}