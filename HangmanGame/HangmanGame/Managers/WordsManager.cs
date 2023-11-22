namespace HangmanGame.Managers;

public class WordsManager : ManagerBase
{
    private readonly string _textFile = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Config\WordPool.txt");
    private readonly string _defaultString = "HangmanGame";

    private string[]? _availableWords;
    
    // Subscriptions
    public override void OnSolutionInitialized(object source, EventArgs eventArgs)
    {
        Setup();
    }
    
    // Methods
    public override void Setup()
    {
        Console.WriteLine("Initializing Word Manager...");
        if (File.Exists(_textFile))
        {
            string wordPoolText = File.ReadAllText(_textFile);
            _availableWords = wordPoolText.Split("-");
        }
    }

    public string GetRandomWord()
    {
        Random random = new Random();
        if (_availableWords != null)
        {
            int randomIndex = random.Next(_availableWords.Length);
            return _availableWords[randomIndex];
        }

        return _defaultString;
    }

    public char[] GetLettersFromWord(string gameWord)
    {
        return gameWord.ToCharArray();
    }
}