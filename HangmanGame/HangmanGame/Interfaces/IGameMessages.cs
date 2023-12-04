namespace HangmanGame.Interfaces;

public interface IGameMessages
{
    public const string EmptyString = "";
    public const string LineBreak = "\n";
    public const string DefaultSeparator = "-";
    public const string DefaultSeparatorSpaced = " - ";
    public const string InitializationMessage = "Initializing Game Manager...";
    public const string FoundAllLettersMessage = "FOUND ALL LETTERS\nThe word was `{0}`";
    public const string AllHangmanPartsAppearedMessage = "All hangman parts appeared, you LOSE!";
    public const string AskForLetterMessage = "\n\nPlease, try to guess a letter: ";
    public const string InvalidKeyMessage = "Invalid key pressed.";
    public const string RepeatedLetterMessage = "\nAlready tried letter: {0}\nPlease use another letter.";
    public const string FoundNewLetterMessage = "\nFOUND A LETTER {0}!";
    public const string WrongGuessMessage = "\nWRONG GUESS!";
    public const string ReturningToMenuMessage = "Returning to menu...";
    public const string UsedLettersSeparatorMessage = "_____________________________\nAlready used letters:";
    public const string ContinueMessage = "Press any key to continue...";
    public const string WinGameMessage = "You've won the game! Congratulations!";
    public const string LoseGameMessage = "You've lose! Good luck next time!";
}