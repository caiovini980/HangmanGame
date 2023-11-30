using HangmanGame.Managers;

namespace HangmanGame.CustomEventArgs;

public class GameStartEventArgs : EventArgs
{
    public char[] RandomWord { get; private set; }
    public InputManager InputManagerInstance { get; private set; }

    private string _exceptionMessage = "Given array is empty.\nPlease provide a filled one.";
    
    public GameStartEventArgs(char[] word, InputManager inputManager)
    {
        if (word.Length <= 0)
        {
            throw new ArgumentException(_exceptionMessage);
        }
        
        RandomWord = word;
        InputManagerInstance = inputManager;
    }
}