namespace HangmanGame.Managers;

public class WordsManager : ManagerBase
{
    // Subscriptions
    public override void OnSolutionInitialized(object source, EventArgs eventArgs)
    {
        Setup();
    }
    
    // Methods
    public override void Setup()
    {
        Console.WriteLine("Initializing Word Manager...");
        // Read the external file
        // Get a random word
    }

    
}