public enum Suit { Hearts, Diamonds, Clubs, Spades, CutCard }

// Set Two = 2 so the enum starts at 2 instead of 0
// Which will make it easier to calculate the hand's total value later
public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
public enum Move { Hit, Stand }
public enum Outcome { Win, Lose, Push, BlackJack }