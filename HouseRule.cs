namespace HouseRules;

class HouseRules
{
    // Store Dealer houserules to stand on S17 (soft) or H17 (hard)
    // Private list so only this class can change it
    private List<string> _hitRules = new List<string>();
    // Public list so others classes can look at the rule but not change them
    public List<string> HitRules { get { return _hitRules; } }

    // Store number of Decks hourserules 
    // testing a similar way to write the DeckRule field and property as for the hitRule, but different format
    public List<string> DeckRules { get; private set; } = new List<string>();

    //Store houserules for remaining number of cards (penetration) in the deck before reshuffling
    public List<string> PenetrationRules { get; private set; } = new List<string>();

    public List<string> N21WinRules { get; private set; } = new List<string>();
    
    public HouseRules()
    {
        
    }
}
