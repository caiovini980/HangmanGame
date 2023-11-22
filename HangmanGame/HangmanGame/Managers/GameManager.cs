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
        SetPlaceholders(splittedWord);
        Console.WriteLine(_wordPlaceholder);

        List<char> usedLetters = new List<char>();
        
        while (!_isGameOver)
        {
            // ask for a letter
            Console.WriteLine("\nPlease, try to guess a letter: ");
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();

            if (!inputManager.IsInputValid(inputKeyInfo, GameStates.Running))
            {
                Console.WriteLine("Invalid key pressed.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            
            char usedLetter = usedLetters.Find(x => x == inputKeyInfo.KeyChar);
            int[] matchedItemsIndexes= splittedWord.FindAllIndexesOf(inputKeyInfo.KeyChar);
            
            if (usedLetter != new char())
            {
                Console.WriteLine("\nAlready tried letter: {0}\nPlease use another letter.", usedLetter);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
                
            if (matchedItemsIndexes.Length > 0)
            {
                // found a letter
                Console.WriteLine("\nFOUND A LETTER {0} on indexes: \n", usedLetter);

                foreach (int index in matchedItemsIndexes)
                {
                    Console.WriteLine(index);
                }
                
                // if a letter is discovered,
                // puts the letter into the _ placeholder
                
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                // error
                Console.WriteLine("WRONG GUESS!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                    
                // if a letter is wrong
                // update the error state
                // change the hangman picture
            }
                
            usedLetters.Add(inputKeyInfo.KeyChar);
        }
        
        OnGameEnded();
    }

    private void SetPlaceholders(char[] selectedWord)
    {
        foreach (char letter in selectedWord)
        {
            _wordPlaceholder += "_" + " ";
        }
    }

    private void Restart()
    {
        
    }

    private void CloseGame()
    {
        
    }
    
    // Event calls
    private void OnGameEnded()
    {
        GameEnded?.Invoke(this, EventArgs.Empty);
        CloseGame();
    }
}