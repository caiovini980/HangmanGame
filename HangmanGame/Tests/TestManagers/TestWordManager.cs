using HangmanGame.Managers;

namespace Tests.TestManagers;

[TestFixture]
public class TestWordManager
{
    WordsManager _wordsManager = new WordsManager();
    
    [SetUp]
    public void Init()
    {
        Assert.That(_wordsManager, Is.Not.Null);
        _wordsManager.Setup();
    }

    [Test]
    public void Must_Return_A_Random_Word_From_WordPool()
    {
        Assert.That(_wordsManager.GetRandomWord(), Is.Not.Null);
    }

    [TestCase("caio")]
    [TestCase("kokku")]
    [TestCase("otorrinolaringologista")]
    [TestCase("testing")]
    [TestCase("Unity Training")]
    public void Must_Return_Letters_From_Given_Word_As_Char_Array(string word)
    {
        Assert.That(_wordsManager.GetLettersFromWord(word).Length, Is.GreaterThan(0));
    }
    
    [TestCase("")]
    public void Must_Return_Not_Return_Letters(string word)
    {
        Assert.That(_wordsManager.GetLettersFromWord(word).Length, Is.EqualTo(0));
    }
    
    [TestCase(" 1234")]
    public void Must_Return_Five_Slots_On_Char_Array(string word)
    {
        Assert.That(_wordsManager.GetLettersFromWord(word).Length, Is.EqualTo(5));
    }
}