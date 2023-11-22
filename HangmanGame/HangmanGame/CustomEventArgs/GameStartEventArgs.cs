using HangmanGame.Managers;

namespace HangmanGame.CustomEventArgs;

public class GameStartEventArgs : EventArgs
{
    public char[] RandomWord { get; private set; }
    public InputManager InputManagerInstance { get; private set; }
    

    public GameStartEventArgs(char[] word, InputManager inputManager)
    {
        RandomWord = word;
        InputManagerInstance = inputManager;
    }
}