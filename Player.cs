public class Player : Participant
{
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Pushes { get; set; }
    public int Blackjacks { get; set; }

    public Player(string name) : base(name) { }

    // keep statistics on the player results, for every round 
    public void AddResult(Outcome outcome)
    {
        if (outcome == Outcome.Win)
        {
            Wins++;
        }
        else if (outcome == Outcome.BlackJack)
        {
            Wins++;
            Blackjacks++;
        }
        else if (outcome == Outcome.Lose)
        {
            Losses++;
        }
        else
        {
            Pushes++;
        }
    }
}