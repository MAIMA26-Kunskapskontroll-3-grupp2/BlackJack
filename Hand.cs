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
    // Check if the hand's total value is over 21
    public bool IsBust()
    {
        return CalculateValue() > 21;
    }
    // Check if an ace is hard or soft
    public bool IsSoft()
    {
        // Step 1: add hand, every ace count as one (hard)
        int valueWithAcesAsOne = 0;
        bool hasAce = false;

        foreach (Card card in _cards)
        {
            if (card.Rank == Rank.Ace)
            {
                valueWithAcesAsOne += 1;
                hasAce = true;
            }
            else if (card.Rank == Rank.Jack || card.Rank == Rank.Queen || card.Rank == Rank.King)
            {
                valueWithAcesAsOne += 10;
            }
            else
            {
                // Two to Ten worth their number
                valueWithAcesAsOne += (int)card.Rank;
            }
        }

        // Step 2: hand cant be soft without an ace, soft = false
        if (!hasAce)
        {
            return false;
        }

        // Step 3: if there are ace(s), try let one be 11 ( = +10 )
        // If the hand is still 21 or less, the hand is soft.
        if (valueWithAcesAsOne + 10 <= 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

// Check if the player got exactly 21 points on their first two cards
public bool IsNatural21()
    {
        return Cards.Count == 2 && CalculateValue() == 21;
    }
}