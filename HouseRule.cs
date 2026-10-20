namespace HouseRules;

class HouseRules
{
    // Store Dealer houserules to stand on S17 (soft) or H17 (hard)
    // Private list so only this class can change it
    private List<string> _hitRules = new List<string> { "HitRules", "S17" }; //standard dealer hit rule = stand on soft 17.
    // Public list so others classes can look at the rule but not change them
    public List<string> HitRules { get { return _hitRules; } }

    // Store number of Decks hourserules 
    // testing a similar way to write the DeckRule field and property as for the hitRule, but different format
    public List<string> DeckRules { get; private set; } = new List<string> { "DeckRules", "One" }; //standard DeckRules = 1 deck of cards

    //Store houserules for remaining number of cards (penetration) in the deck before reshuffling
    public List<string> PenetrationRules { get; private set; } = new List<string> { "PenetrationRules", "cut15" }; //standard PenetrationRules = cut when 15 cards left

    public List<string> N21WinRules { get; private set; } = new List<string> { "N21WinRules", "N21Win" }; //standard (BlackJack) N21WinRules = Natural 21 wins

    // Path to external File containing Houserules
    readonly string filepath = "./RuleSet.txt";

    private ConsoleUi _ui = new ConsoleUi();

    public HouseRules(string filepath)
    {
        try
        {
            string content = File.ReadAllText(filepath);
        }
        catch (FileNotFoundException ex)
        {
            _ui.ShowErrorMessage($"Error: The House-Rule file '{ex.FileName}' could not be found.");
            _ui.ShowMessage("Default house rules loaded:\n1. Dealer stand at S17\n2. 1-Deck game\n3. Cut-card = 15 cards\n4. Natural 21 (N21) wins");
        }
    }
}
