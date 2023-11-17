using HangmanGame.ENUMs;

namespace HangmanGame.Managers;

public class InputManager : ManagerBase
{
    private List<ConsoleKey> _menuKeys = new List<ConsoleKey>();
    private List<ConsoleKey> _gameKeys = new List<ConsoleKey>();
    
    public override void Setup()
    {
        _menuKeys.Add(ConsoleKey.NumPad1);
        _menuKeys.Add(ConsoleKey.NumPad0);
        _menuKeys.Add(ConsoleKey.D1);
        _menuKeys.Add(ConsoleKey.D0);
        
        // for each letter on the alphabet and number
        // add to the gameKeys
    }
    
    public bool IsInputValid(ConsoleKeyInfo keyInfo, GameStates gameState)
    {
        // check inputs for menu
        if (gameState == GameStates.Menu) { return ValidateInputInCollection(keyInfo, _menuKeys); }

        // check inputs for the game
        if (gameState == GameStates.Running) { return ValidateInputInCollection(keyInfo, _gameKeys); }
        
        return false;
    }

    private bool ValidateInputInCollection(ConsoleKeyInfo keyInfo, List<ConsoleKey> collection)
    {
        foreach (ConsoleKey consoleKey in collection)
        {
            Console.WriteLine($"{consoleKey} == {keyInfo.Key.ToString()}");
            if (keyInfo.Key == consoleKey) { return true; }
        }
        
        return false;
    }
}