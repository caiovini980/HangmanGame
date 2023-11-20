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
    public void OnGameStarted(object source, EventArgs eventArgs)
    {
        Play();
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

    private void Play()
    {
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
    protected virtual void OnGameEnded()
    {
        GameEnded?.Invoke(this, EventArgs.Empty);
    }
}