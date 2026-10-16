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

    // Calculate the sum value and handle soft/hard aces
    public int CalculateValue()
    {
        int score = 0;
        int aces = 0;

        foreach (Card card in _cards)
        {
            switch (card.Rank)
            {
                case Rank.Two:
                case Rank.Three:
                case Rank.Four:
                case Rank.Five:
                case Rank.Six:
                case Rank.Seven:
                case Rank.Eight:
                case Rank.Nine:
                case Rank.Ten:
                    score += (int)card.Rank;
                    break;

                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    score += 10;
                    break;

                case Rank.Ace:
                    score += 11;
                    aces++;
                    break;
            }
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

    // Check if an ace is hard or soft
    public bool IsSoft()
    {
        return false; // Temporary placeholder
    }

    // Check if the player got exactly 21 points on their first two cards
    public bool IsNatural21()
    {
        return false; // Temporary placeholder
    }
}