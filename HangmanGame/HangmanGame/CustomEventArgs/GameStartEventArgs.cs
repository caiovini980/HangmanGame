namespace HangmanGame.CustomEventArgs;

public class GameStartEventArgs : EventArgs
{
    public char[] RandomWord { get; set; }

    public GameStartEventArgs(char[] word)
    {
        RandomWord = word;
    }
}