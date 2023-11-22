using HangmanGame.CustomEventArgs;
using HangmanGame.ENUMs;
using HangmanGame.Utils;

namespace HangmanGame.Managers;

public class GameManager : ManagerBase
{
    // Delegates
    public delegate void GameEndedEventHandler(object source, EventArgs args);

    // Events
    public event GameEndedEventHandler? GameEnded;
    
    // Variables
    private bool _isGameOver = false;
    private string _wordPlaceholder = "";
    private int _lettersFoundPerTurn = 1;
    
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
    }

    private void Play(char[] splittedWord, InputManager inputManager)
    {
        List<char> usedLetters = new List<char>();
        int numberOfCorrectLetters = 0;
        bool isGameOver = false;
        bool haveWon = false;
        SetPlaceholders(splittedWord);
        
        while (!isGameOver)
        {
            // ask for a letter
            Console.WriteLine(_wordPlaceholder);
            Console.WriteLine("\n\nPlease, try to guess a letter: ");
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();

            if (!inputManager.IsInputValid(inputKeyInfo, GameStates.Running))
            {
                Console.WriteLine("Invalid key pressed.");
                ShowEndTurnSection();
                continue;
            }
            
            char usedLetter = usedLetters.Find(x => x == inputKeyInfo.KeyChar);
            int[] matchedItemsIndexes= splittedWord.FindAllIndexesOf(inputKeyInfo.KeyChar);
            
            if (usedLetter != new char())
            {
                Console.WriteLine("\nAlready tried letter: {0}\nPlease use another letter.", usedLetter);
                ShowEndTurnSection();
                continue;
            }
                
            if (matchedItemsIndexes.Length > 0)
            {
                Console.WriteLine("\nFOUND A LETTER {0}!", usedLetter);
                
                UpdatePlaceholdersWithInputAt(matchedItemsIndexes, inputKeyInfo.KeyChar);
                ShowEndTurnSection();
                numberOfCorrectLetters += _lettersFoundPerTurn;
            }
            else
            {
                // error
                Console.WriteLine("\nWRONG GUESS!");
                ShowEndTurnSection();

                // if a letter is wrong
                // update the error state
                // change the hangman picture
            }

            // if all letters were found = WIN
            if (numberOfCorrectLetters >= splittedWord.Length)
            {
                isGameOver = true;
                haveWon = true;
            }
            
            // if reached all errors = LOSE
            
            usedLetters.Add(inputKeyInfo.KeyChar);
        }
        
        OnGameEnded(haveWon);
    }

    private void ShowEndTurnSection()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    private void SetPlaceholders(char[] selectedWord)
    {
        foreach (char letter in selectedWord)
        {
            _wordPlaceholder += "-";
        }
    }

    private void UpdatePlaceholdersWithInputAt(int[] indexes, char input)
    {
        char[] array = _wordPlaceholder.ToCharArray();
        _wordPlaceholder = "";
        
        foreach (char index in indexes)
        {
            array[index] = input;
        }

        _wordPlaceholder = new string(array);
    }

    private void Restart()
    {
        
    }

    private void CloseGame(bool haveWon)
    {
        
    }
    
    // Event calls
    private void OnGameEnded(bool haveWon)
    {
        GameEnded?.Invoke(this, EventArgs.Empty);
        CloseGame(haveWon);
    }
}