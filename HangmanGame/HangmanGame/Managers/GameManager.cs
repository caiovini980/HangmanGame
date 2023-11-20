using HangmanGame.CustomEventArgs;

namespace HangmanGame.Managers;

public class GameManager : ManagerBase
{
    // Delegates
    public delegate void GameEndedEventHandler(object source, EventArgs args);

    // Events
    public event GameEndedEventHandler? GameEnded;
    
    // Variables
    private bool _isGameOver = false;
    
    // Subscriptions
    public void OnGameStarted(object source, GameStartEventArgs eventArgs)
    {
        Play(eventArgs.RandomWord);
    }
    
    public override void OnSolutionInitialized(object source, EventArgs eventArgs)
    {
        Setup();
    }
    
    // Methods
    public override void Setup()
    {
        Console.WriteLine("Initializing Game Manager...");
    }

    private void Play(char[] selectedWord)
    {
        Console.WriteLine("Selected word letters are: ");
        foreach (var letter in selectedWord)
        {
            Console.WriteLine(letter);
        }
        
        while (!_isGameOver)
        {
            // keeps running the game
            
            
        }
        OnGameEnded();
    }

    private void Restart()
    {
        
    }

    private void CloseGame()
    {
        
    }
    
    // Event calls
    private void OnGameEnded()
    {
        GameEnded?.Invoke(this, EventArgs.Empty);
        CloseGame();
    }
}