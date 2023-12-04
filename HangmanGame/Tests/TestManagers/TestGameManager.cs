using HangmanGame.CustomEventArgs;
using HangmanGame.Managers;

namespace Tests.TestManagers;

[TestFixture]
public class TestGameManager
{
    private GameManager _gameManager = new GameManager();
    private InputManager _inputManager = new InputManager();
    
    [SetUp]
    public void Setup()
    {
        Assert.IsInstanceOf<ManagerBase>(_gameManager);
        Assert.That(_gameManager, Is.Not.Null);
        Assert.That(_inputManager, Is.Not.Null);
        
        _inputManager.Setup();
        _gameManager.Setup();
    }

    [TestCase(new [] {'k', 'o', 'k', 'k', 'u'})]
    [TestCase(new [] {'t', 'e', 's', 't', 's'})]
    [TestCase(new [] {'o', 't', 'o', 'r', 'r', 'i', 'n', 'o'})]
    [TestCase(new [] {'}', '+', '_', '-', ' '})]
    public void Must_Create_EventArgs_Correctly(char[] wordLetters)
    {
        GameStartEventArgs args = new GameStartEventArgs(wordLetters, _inputManager);
        Assert.That(args, Is.Not.Null);
    }

    [TestCase(new char[]{})]
    public void Must_Create_EventArgs_Incorrectly(char[] wordLetters)
    {
        Assert.Throws<ArgumentException>(() => 
            new GameStartEventArgs(wordLetters, _inputManager));
    }

    [Test]
    public void Must_Start_Game_Correctly()
    {
        char[] testArray = {'t', 'e', 's', 't', 's'};
        GameStartEventArgs args = new GameStartEventArgs(testArray, _inputManager);
        Assert.DoesNotThrow(() => _gameManager.OnGameStarted(this, args));
    }
    
    [Test]
    public void Must_Not_Start_Game_Correctly()
    {
        char[] testArray = {'t', 'e', 's', 't', 's'};
        GameStartEventArgs args = new GameStartEventArgs(testArray, null);
        Assert.Throws<NullReferenceException>(() => _gameManager.OnGameStarted(this, null));
    }
}