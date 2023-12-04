using HangmanGame.ENUMs;
using System.Text.RegularExpressions;

namespace HangmanGame.Managers;

public class InputManager : ManagerBase
{
    private readonly string _gamePattern = "^[a-zA-Z0-9]$";
    
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
        string keyString = keyInfo.Key.ToString();
        string lastCharacter = keyString.Substring(keyString.Length - 1);
        string secondLastCharacter = String.Empty;

        if (keyString.Length > 1)
        {
            secondLastCharacter = keyString.Substring(keyString.Length - 2);
        }
        
        if (!int.TryParse(lastCharacter, out int _) && secondLastCharacter != String.Empty)
        {
            return false;
        }

        if (Regex.IsMatch(lastCharacter, @_gamePattern) || 
            Regex.IsMatch(lastCharacter.ToLower(), @_gamePattern))
        {
            return true;
        }
        
        return false;
    }
    
    private bool ValidateMenuInput(ConsoleKeyInfo keyInfo)
    {
        return keyInfo.KeyChar.ToString() == ((int)MenuOptions.StartGame).ToString() || 
               keyInfo.KeyChar.ToString() == ((int)MenuOptions.CloseGame).ToString();
    }
}