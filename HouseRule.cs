public class HouseRules
{
    //--------------- Rules-------------
    // Other class can read the rules (get), but only this class can change them (private set)
    // Store Dealer houserules to stand on S17 (soft) or H17 (hard)
    // Values after "=" are standard/default rules - used only if the RuleSet file is missing

    //True = dealer hits on soft 17 (H17). False = dealer standd on soft 17 (s17)
    public bool DealerHitsSoft17 { get; private set; } = false;

    // Number of deck(s) in the shoe, i.e. 1, 6, or 8
    public int NumberOfDecks { get; private set; } = 1;

    // Shoe is reshuffled when number of cards left is between MinCutCard and MaxCutCard
    //if both are 15, the shoe is always reshuffled when 15 cards are left
    public int MinCutCard { get; private set; } = 15;
    public int MaxCutCard { get; private set; } = 15;

    // True = natural 21 beats a 21 with three cards or more (N21)
    // False = all 21 are worth the same (21)
    public bool NaturalBeats21 { get; private set; } = true;

    private ConsoleUi _ui = new ConsoleUi();

    // ----------Constructor-----------
    // rulesetName = the rule set to use, i.e. "Houserule_1"
    // If ruleSetName is empty, the first rule set in the file is used = the "Standard" rules
    public HouseRules(string ruleSetName = "", string filepath = ".RuleSet.txt")
    {
        try
        {
            string[] lines = File.ReadAllLines(filepath);
            LoadRules(lines, ruleSetName);
        }
        catch (FileNotFoundException ex)
        {
            _ui.ShowErrorMessage($"Error: The House-Rule file '{ex.FileName}' could not be found.");
            ShowRules("Default");
        }
    }

    //------------ Rule set names ------------
    //Returns names of all rule sets in teh file, for example "Standard" and "Houserule_1"
    //If the file is missing, an empty list is returnd
    public static List<string> GetRuleSetNames(string filepath = "./RuleSet.txt")
    {
        List<string> names = new List<string>();
        if (!File.Exists(filepath))
        {
            return names;
        }

        string[] lines = File.ReadAllLines(filepath);

        foreach (string line in lines)
        {
            //trim() removes spaces at the start and the end of the line
            string trimmedLine = line.Trim();

            if (trimmedLine.StartsWith("-"))
            {
                //skip first character Substring(1)
                string name = trimmedLine.Substring(1).Trim();
                names.Add(name);
            }
        }

        return names;
    }

    //-----------Read the file -----------
    //jumps throught he lines in the file and saves the rules from user shosen rule set
    private void LoadRules(string[] lines, string chosenRuleset)
    {
        //true while reading lines of chosen rule set
        bool isInChosenRuleSet = false;
        //true if the chosen rule set is found in the file
        bool found = false;

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();

            //skip empty lines
            if (trimmedLine == "")
            {
                continue;
            }

            // lines starting with "-" si the name of a new rule set in the file
            if (trimmedLine.StartsWith("-"))
            {
                string ruleSetName = trimmedLine.Substring(1).Trim();

                // if no rule set was chosen, use the first one in the file
                if (chosenRuleset == "")
                {
                    chosenRuleset = ruleSetName;
                }

                isInChosenRuleSet = ruleSetName == chosenRuleset;

                if (isInChosenRuleSet)
                {
                    found = true;
                }

                continue;
            }

            // skip rules that belong to other rule set, other rules not yet implemented
            if (!isInChosenRuleSet)
            {
                continue;
            }

            // a rule line has this pattern abc=xyz (i.e. "HitRules=S17")
            // split on "=" results in two parts (I) rule name "HitRules", and (II) rule value "S17"
            string[] parts = trimmedLine.Split('=');

            if (parts.Length != 2)
            {
                _ui.ShowErrorMessage($"Error: Invalid line in the House-Rule file: '{trimmedLine}'");
                continue;
            }

            string ruleName = parts[0].Trim();
            string ruleValue = parts[1].Trim();

            SetRule(ruleName, ruleValue);
        }

        if (!found)
        {
            _ui.ShowErrorMessage($"Error: The house rule set '{chosenRuleset}' could not be found.");
            ShowRules("Default");
            return;
        }

        // reshuffle point must be smaller than the number of cards in the shoe (52 cards/deck)
        int cardsInShoe = NumberOfDecks * 52;
        if (MaxCutCard >= cardsInShoe)
        {
            _ui.ShowErrorMessage($"Error: Cut-card {MaxCutCard} is too large for {NumberOfDecks} deck(s). Using 15.");
            MinCutCard = 15;
            MaxCutCard = 15;
        }

        ShowRules(chosenRuleset);
    }

    //--------------Save one rule-----------
    // send the value to the method that handels that rule
    // i.e. ruleName "HitRules" and ruleValue "H17" -> SetHitRule("H17")
    private void SetRule(string ruleName, string ruleValue)
    {
        if (ruleValue == "null")
        {
            return;
        }

        if (ruleName == "HitRules")
        {
            SetHitRule(ruleValue);
        }
        else if (ruleName == "DeckRules")
        {
            SetDeckRule(ruleValue);
        }
        else if (ruleName == "PenetrationRules")
        {
            SetPenetrationRule(ruleValue);
        }
        else if (ruleName == "N21WinRules")
        {
            SetN21WinRule(ruleValue);
        }
        else
        {
            _ui.ShowErrorMessage($"Error: Unknown house rule '{ruleName}'.");
        }
    }

    // allowed values "S17" or H17
    private void SetHitRule(string value)
    {
        if (value == "H17")
        {
            DealerHitsSoft17 = true;
        }
        else if (value == "S17")
        {
            DealerHitsSoft17 = false;
        }
        else
        {
            ShowInvalidValue("HitRules", value);
        }
    }

    // Allowed values: a number bigger than 0, for example "6"
    private void SetDeckRule(string value)
    {
        // TryParse returns true if the text is a number, and puts the number in "decks"
        bool isNumber = int.TryParse(value, out int decks);

        if (isNumber && decks > 0)
        {
            NumberOfDecks = decks;
        }
        else
        {
            ShowInvalidValue("DeckRules", value);
        }
    }

    // Allowed values: one number ("15") or an interval ("47to78")
    private void SetPenetrationRule(string value)
    {
        // Split("to") turns "47to78" into "47" and "78", and "15" into only "15"
        string[] numbers = value.Split("to");

        // One number, for example "15"
        if (numbers.Length == 1)
        {
            bool isNumber = int.TryParse(numbers[0], out int cutCard);

            if (isNumber && cutCard >= 0)
            {
                MinCutCard = cutCard;
                MaxCutCard = cutCard;
                return;
            }
        }

        // Two numbers, for example "47to78"
        if (numbers.Length == 2)
        {
            bool minIsNumber = int.TryParse(numbers[0], out int min);
            bool maxIsNumber = int.TryParse(numbers[1], out int max);

            if (minIsNumber && maxIsNumber && min >= 0 && max >= min)
            {
                MinCutCard = min;
                MaxCutCard = max;
                return;
            }
        }

        // If we get here, the value was not allowed
        ShowInvalidValue("PenetrationRules", value);
    }

    // Allowed values: "N21" or "21"
    private void SetN21WinRule(string value)
    {
        if (value == "N21")
        {
            NaturalBeats21 = true;
        }
        else if (value == "21")
        {
            NaturalBeats21 = false;
        }
        else
        {
            ShowInvalidValue("N21WinRules", value);
        }
    }

    // ---------------- Messages ----------------
    private void ShowInvalidValue(string ruleName, string value)
    {
        _ui.ShowErrorMessage($"Error: Invalid value '{value}' for {ruleName}. The standard value is used.");
    }

    // Shows the rules that will be used in the game
    private void ShowRules(string ruleSetName)
    {
        string hitRule = "Dealer stands on soft 17 (S17)";
        if (DealerHitsSoft17)
        {
            hitRule = "Dealer hits on soft 17 (H17)";
        }

        string cutCard = $"{MinCutCard} cards";
        if (MinCutCard != MaxCutCard)
        {
            cutCard = $"{MinCutCard} to {MaxCutCard} cards";
        }

        string winRule = "All 21 are worth the same (21)";
        if (NaturalBeats21)
        {
            winRule = "Natural 21 wins (N21)";
        }

        _ui.ShowMessage($"House rules '{ruleSetName}' loaded:\n1. {hitRule}\n2. {NumberOfDecks} deck(s)\n3. Cut-card = {cutCard}\n4. {winRule}");
    }
}
