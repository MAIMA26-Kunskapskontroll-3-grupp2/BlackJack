public abstract class Participant
{
    public string Name { get; }
    public Hand Hand { get; } = new Hand();

    protected Participant(string name)
    {
        Name = name;
    }

    public void AddCard(Card card)
    {
        Hand.AddCard(card);
    }
    public void ClearHand()
    {
        Hand.Clear();
    }
}