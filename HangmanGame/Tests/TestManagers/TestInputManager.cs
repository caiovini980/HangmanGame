using HangmanGame.ENUMs;
using HangmanGame.Managers;

namespace Tests.TestManagers;

[TestFixture]
public class TestInputManager
{
    InputManager _inputManager = new InputManager();
    
    [SetUp]
    public void Init()
    {
        Assert.IsInstanceOf<ManagerBase>(_inputManager);
        Assert.That(_inputManager, Is.Not.Null);
        _inputManager.Setup();
    }

    private readonly Dictionary<ConsoleKey, char> _menuKeyCharCorrectRelation = new Dictionary<ConsoleKey, char>()
    {
        {ConsoleKey.D0, '0'},
        {ConsoleKey.D1, '1'},
        {ConsoleKey.NumPad0, '0'},
        {ConsoleKey.NumPad1, '1'},
    };
    
    private readonly Dictionary<ConsoleKey, char> _menuKeyCharWrongRelation = new Dictionary<ConsoleKey, char>()
    {
        {ConsoleKey.D2, '2'},
        {ConsoleKey.D8, '8'},
        {ConsoleKey.A, 'A'},
        {ConsoleKey.Enter, ' '},
    };
    
    private readonly Dictionary<ConsoleKey, char> _gameKeyCharCorrectRelation = new Dictionary<ConsoleKey, char>()
    {
        {ConsoleKey.D0, '0'},
        {ConsoleKey.A, 'A'},
        {ConsoleKey.NumPad7, '7'},
        {ConsoleKey.O, 'O'},
    };
    
    private readonly Dictionary<ConsoleKey, char> _gameKeyCharWrongRelation = new Dictionary<ConsoleKey, char>()
    {
        {ConsoleKey.Enter, '\r'},
        {ConsoleKey.Home, '\b'},
        {ConsoleKey.Backspace, '\0'},
        {ConsoleKey.Delete, '\0'},
    };

    [TestCase(ConsoleKey.D0, GameStates.Menu)]
    [TestCase(ConsoleKey.D1, GameStates.Menu)]
    [TestCase(ConsoleKey.NumPad0, GameStates.Menu)]
    [TestCase(ConsoleKey.NumPad1, GameStates.Menu)]
    public void Input_Must_Be_Valid_For_Menu(ConsoleKey key, GameStates state)
    {
        ConsoleKeyInfo info =  new ConsoleKeyInfo(_menuKeyCharCorrectRelation[key], key, false, false, false);
        bool isValid = _inputManager.IsInputValid(info, state);
        
        Assert.That(isValid, Is.EqualTo(true));
    }
    
    [TestCase(ConsoleKey.D2, GameStates.Menu)]
    [TestCase(ConsoleKey.D8, GameStates.Menu)]
    [TestCase(ConsoleKey.A, GameStates.Menu)]
    [TestCase(ConsoleKey.Enter, GameStates.Menu)]
    public void Input_Must_Be_Invalid_For_Menu(ConsoleKey key, GameStates state)
    {
        ConsoleKeyInfo info =  new ConsoleKeyInfo(_menuKeyCharWrongRelation[key], key, false, false, false);
        bool isValid = _inputManager.IsInputValid(info, state);
        
        Assert.That(isValid, Is.EqualTo(false));
    }
    
    [TestCase(ConsoleKey.D0, GameStates.Running)]
    [TestCase(ConsoleKey.NumPad7, GameStates.Running)]
    [TestCase(ConsoleKey.A, GameStates.Running)]
    [TestCase(ConsoleKey.O, GameStates.Running)]
    public void Input_Must_Be_Valid_For_Game(ConsoleKey key, GameStates state)
    {
        ConsoleKeyInfo info =  new ConsoleKeyInfo(_gameKeyCharCorrectRelation[key], key, false, false, false);
        bool isValid = _inputManager.IsInputValid(info, state);
        
        Assert.That(isValid, Is.EqualTo(true));
    }
    
    [TestCase(ConsoleKey.Enter, GameStates.Running)]
    [TestCase(ConsoleKey.Home, GameStates.Running)]
    [TestCase(ConsoleKey.Backspace, GameStates.Running)]
    [TestCase(ConsoleKey.Delete, GameStates.Running)]
    public void Input_Must_Be_Invalid_For_Game(ConsoleKey key, GameStates state)
    {
        ConsoleKeyInfo info =  new ConsoleKeyInfo(_gameKeyCharWrongRelation[key], key, false, false, false);
        bool isValid = _inputManager.IsInputValid(info, state);
        
        Assert.That(isValid, Is.EqualTo(false));
    }
}