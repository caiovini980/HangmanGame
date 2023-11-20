namespace HangmanGame.CustomEventArgs;

public class GameStartEventArgs : EventArgs
{
    public string RandomWord { get; set; }

    public GameStartEventArgs(string word)
    {
        RandomWord = word;
    }
}