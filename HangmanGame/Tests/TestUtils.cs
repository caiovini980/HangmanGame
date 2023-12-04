using HangmanGame.Managers;
using HangmanGame.Utils;

namespace Tests;

[TestFixture]
public class TestUtils
{
    readonly WordsManager _wordsManager = new WordsManager();
    
    [SetUp]
    public void Init()
    {
        Assert.That(_wordsManager, Is.Not.Null);
    }
    
    // Testing Utils 
    [TestCase("o", "kokku")]
    public void Must_Return_One_Instance_Of_Letter(char letter, string word)
    {
        char[] letters = _wordsManager.GetLettersFromWord(word);
        
        Assert.That(letters.FindAllIndexesOf(letter).Length, Is.EqualTo(1)); 
    }

    [TestCase(new int[] {1, 2, 3, 4, 5}, 1)]
    [TestCase(new int[] {1, 1, 1, 1, 1}, 1)]
    [TestCase(new int[] {1, 2, 1, 2, 1}, 1)]
    [TestCase(new int[] {1, 2, 11, 12, 111}, 1)]
    public void Must_Find_At_Least_One_Integer(int[] numbers, int singleNumber)
    {
        Assert.That(numbers.FindAllIndexesOf(singleNumber).Length, Is.GreaterThan(0)); 
    }
    
    [TestCase(new int[] {11, 12, 13, 123, 123123, 1231232323}, 1)]
    
    public void Must_Find_No_Integers(int[] numbers, int singleNumber)
    {
        Assert.That(numbers.FindAllIndexesOf(singleNumber).Length, Is.EqualTo(0)); 
    }
    
}