using System.Text.RegularExpressions;
using HangmanGame.CustomEventArgs;
using HangmanGame.ENUMs;
using HangmanGame.Utils;

namespace HangmanGame.Managers;

public class GameManager : ManagerBase
{
    // Variables
    private string[]? _hangImages;
    private string _wordPlaceholder = "";
    private readonly string _initialPlaceholder = "";
    
    // File Variables
    private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Config\HangmanFigures.txt");
    private readonly string _separator = ";";
    
    // Subscriptions
    public void OnGameStarted(object source, GameStartEventArgs eventArgs)
    {
        Play(eventArgs.RandomWord, eventArgs.InputManagerInstance);
    }
    
    public override void OnSolutionInitialized(object source, EventArgs eventArgs)
    {
        Setup();
    }
    
    // Methods
    public override void Setup()
    {
        Console.WriteLine("Initializing Game Manager...");
        _hangImages = FileUtils.GetSeparatedStrings(_separator, _path);
    }

    private void Play(char[] splittedWord, InputManager inputManager)
    {
        if (_hangImages == null)
        {
            Console.Error.WriteLine("Can't find path to Hangman Images, please provide the correct path.");
            return;
        }
        
        List<char> usedLetters = new List<char>();
        
        int numberOfUniqueLetters = splittedWord.Distinct().Count();
        int lettersFoundPerTurn = 1;
        int numberOfCorrectLetters = 0;
        int numberOfErrors = 0;
        int maxNumberOfErrors = _hangImages.Length - 1;
            
        bool winConditionReached = numberOfCorrectLetters >= numberOfUniqueLetters;
        bool loseConditionReached = numberOfErrors >= maxNumberOfErrors;
        bool isGameOver = false;
        bool haveWon = false;
        
        SetPlaceholders(splittedWord);
        ShowHangman(numberOfErrors);
        
        while (!isGameOver)
        {
            Console.WriteLine("number of correct letters: {0}\nnumber of unique letters: {1}", numberOfCorrectLetters, numberOfUniqueLetters);
            
            Console.WriteLine(_wordPlaceholder);
            Console.WriteLine("\n\nPlease, try to guess a letter: ");
            
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();
            bool isInputValidForRunningGame = inputManager.IsInputValid(inputKeyInfo, GameStates.Running);

            if (!isInputValidForRunningGame)
            {
                Console.WriteLine("Invalid key pressed.");
                ShowEndTurnSection();
                continue;
            }
            
            char usedLetter = usedLetters.Find(x => x == inputKeyInfo.KeyChar);
            int[] matchedItemsIndexes= splittedWord.FindAllIndexesOf(inputKeyInfo.KeyChar);
            bool isGuessOnSelectedWord = matchedItemsIndexes.Length > 0;
            bool isGuessANewLetter = usedLetter == new char();
            
            if (!isGuessANewLetter)
            {
                Console.WriteLine("\nAlready tried letter: {0}\nPlease use another letter.", usedLetter);
                ShowEndTurnSection();
                continue;
            }
                
            if (isGuessOnSelectedWord)
            {
                Console.WriteLine("\nFOUND A LETTER {0}!", usedLetter);
                
                UpdatePlaceholdersWithInputAt(matchedItemsIndexes, inputKeyInfo.KeyChar);
                ShowEndTurnSection();
                numberOfCorrectLetters += lettersFoundPerTurn; 
            }
            else
            {
                Console.WriteLine("\nWRONG GUESS!");
                numberOfErrors += 1;
                ShowEndTurnSection();
            }

            if (winConditionReached)
            {
                Console.WriteLine("FOUND ALL LETTERS");
                isGameOver = true;
                haveWon = true;
            }
            
            if (loseConditionReached)
            {
                Console.WriteLine("All hangman parts appeared, you LOSE!");
                isGameOver = true;
                haveWon = false;
            }
            
            usedLetters.Add(inputKeyInfo.KeyChar);
            ShowHangman(numberOfErrors);
            Console.ReadKey();
        }
        
        
        ShowEndgameMessage(haveWon);
        Console.WriteLine("Returning to menu...");
        ShowEndTurnSection();
    }

    private void ShowHangman(int numberOfErrors)
    {
        if (_hangImages == null) { return; }
        Console.WriteLine(Regex.Unescape(_hangImages[numberOfErrors]));
    }

    private void ShowEndTurnSection()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    private void SetPlaceholders(char[] selectedWord)
    {
        _wordPlaceholder = _initialPlaceholder;
        foreach (char unused in selectedWord)
        {
            _wordPlaceholder += "-";
        }
    }

    private void UpdatePlaceholdersWithInputAt(int[] indexes, char input)
    {
        char[] auxArray = _wordPlaceholder.ToCharArray();
        _wordPlaceholder = "";
        
        foreach (char index in indexes)
        {
            auxArray[index] = input;
        }

        _wordPlaceholder = new string(auxArray);
    }

    private void ShowEndgameMessage(bool haveWon)
    {
        if (haveWon)
        {
            Console.WriteLine("You've won the game! Congratulations!");
            return;
        }
        
        Console.WriteLine("You've lose! Good luck next time!");
    }
}