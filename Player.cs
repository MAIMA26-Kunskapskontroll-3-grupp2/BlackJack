public class Player : Participant
{
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Pushes { get; set; }

    public Player(string name) : base(name) { }

    // keep statistics on the player results, for every round 
    public void AddResult(Outcome outcome)
    {
        if (outcome == Outcome.Win || outcome == Outcome.BlackJack)
        {
            Wins++;
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