using HangmanGame.ENUMs;
using System.Text.RegularExpressions;

namespace HangmanGame.Managers;

public class InputManager : ManagerBase
{
    private List<ConsoleKey> _menuKeys = new List<ConsoleKey>();
    
    private readonly int _numPadMinValueIndex = 96;
    private readonly int _upperNumbersMinValueIndex = 48;

    private readonly int _upperNumbersOne = 49;
    private readonly int _numPadOne = 97;

    private readonly string _gamePattern = "^[a-zA-Z0-9]+$";
        
    public override void Setup()
    {
        // Setup menu keys (0 - 1)
        _menuKeys.Add((ConsoleKey)_upperNumbersMinValueIndex);
        _menuKeys.Add((ConsoleKey)_upperNumbersOne);
        _menuKeys.Add((ConsoleKey)_numPadMinValueIndex);
        _menuKeys.Add((ConsoleKey)_numPadOne);
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
        foreach (ConsoleKey consoleKey in _menuKeys)
        {
            // TODO remove this
            // Console.WriteLine($"{consoleKey} == {keyInfo.Key.ToString()}");
            if (keyInfo.Key == consoleKey) { return true; }
        }
        
        return false;
    }
}