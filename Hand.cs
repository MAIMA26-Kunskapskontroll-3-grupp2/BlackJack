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

    public int CalculateValue()
    {
        // Calculate the sum value and handle soft/hard aces
        // return 0 is a placeholder for a real value to be returned later 
        return 0;
    }

    // Check if the hand's total value is over 21 
    public bool IsBust()
    {
        // Test code - When busted
        return true;

        // Test code - When not busted
        // return false;

        // return CalculateValue() > 21;
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