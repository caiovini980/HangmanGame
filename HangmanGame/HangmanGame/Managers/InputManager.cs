using HangmanGame.ENUMs;
using System.Text.RegularExpressions;

namespace HangmanGame.Managers;

public class InputManager : ManagerBase
{
    private readonly string _gamePattern = "^[a-zA-Z0-9]+$";
    
    // Subscriptions
    public override void OnSolutionInitialized(object source, EventArgs eventArgs)
    {
        Setup();
    }
    
    // Methods
    public override void Setup()
    {
        Console.WriteLine("Initializing Input Manager...");
    }

    public bool IsInputValid(ConsoleKeyInfo keyInfo, GameStates gameState)
    {
        // check inputs for menu
        if (gameState == GameStates.Menu) { return ValidateMenuInput(keyInfo); }

        // check inputs for the game
        if (gameState == GameStates.Running) { return ValidateGameInput(keyInfo); }
        
        return false;
    }
    
    private bool ValidateGameInput(ConsoleKeyInfo keyInfo)
    {
        return Regex.IsMatch(keyInfo.Key.ToString(), @_gamePattern);
    }
    
    private bool ValidateMenuInput(ConsoleKeyInfo keyInfo)
    {
        return keyInfo.KeyChar.ToString() == ((int)MenuOptions.StartGame).ToString() || 
               keyInfo.KeyChar.ToString() == ((int)MenuOptions.CloseGame).ToString();
    }
}