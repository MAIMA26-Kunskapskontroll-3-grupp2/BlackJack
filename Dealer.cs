public class Dealer : Participant
{
    private HouseRules _rules;
    // Sends "Givaren" automatically to the base class, so we don't have to
    // type a name manually whenever we create a new Dealer
    public Dealer(HouseRules rules) : base("Givaren")
    {
        _rules = rules;
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