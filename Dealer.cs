public class Dealer : Participant
{
    private HouseRules _rules;
    // Sends "Givaren" automatically to the base class, so we don't have to
    // type a name manually whenever we create a new Dealer
    public Dealer(HouseRules rules) : base("Givaren")
    {
        _rules = rules;
    }

    public Card? UpCard()
    {
        // Check if the dealer has any cards on hand, and return the first card
        // (the visible upcard) if it exists, otherwise return null
        if (Hand.Cards.Count > 0)
        {
            return Hand.Cards[0];
        }
        else
        {
            return null;
        }
    }

    public Card? HoleCard()
    {
        // Check if the dealer has at least 2 cards on hand and return the second card
        // (the hidden hole card), otherwise return null 
        if (Hand.Cards.Count > 1)
        {
            return Hand.Cards[1];
        }
        else
        {
            return null;
        }
    }

    // Check if the dealer should hit or stand 
    // (if sum value is 17 or above, return false)
    // (if below 17, return true)
    public bool ShouldHit()
    {
        int value = Hand.CalculateValue();
        if (value == 17 && Hand.IsSoft() && _rules.DealerHitsSoft17)
        {
            return true;
        }
        return value < 17;
    }
}