namespace HangmanGame.Managers;

public abstract class ManagerBase
{
    public abstract void Setup();
    
    // Event subscriptions
    public abstract void OnSolutionInitialized(object source, EventArgs eventArgs);
}