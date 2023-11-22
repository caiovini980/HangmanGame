using HangmanGame.CustomEventArgs;
using HangmanGame.ENUMs;

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
        /*
         * Pegar o array de letras que vem e adicionar em um dicionario pra relacionar letra com posição da letra
         * depois só pesquisar se a letra existe no dicionário, e substituir o placeholder na posição salva
         */
        
        string connectedWord = UpdateWord(splittedWord, null, _wordPlaceholder);
        Console.WriteLine(connectedWord);

        while (!_isGameOver)
        {
            // ask for a letter
            Console.WriteLine("Please, try to guess a letter: ");
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();

            if (inputManager.IsInputValid(inputKeyInfo, GameStates.Running))
            {
                
            }
            
            // if a letter is discovered,
                // puts the letter into the _ placeholder
                // add the letter into the "used" letters list
            
            // if a letter is wrong
                // update the error state
                    // change the hangman picture
                // add the letter into the "used" letters list
                
            // If a letter has already been used
                // Show a message saying that this letter was already used
                
            Console.ReadKey();

        }
        OnGameEnded();
    }

    private string UpdateWord(char[] selectedWord, char? letterToCheck, string wordLinked)
    {
        foreach (char letter in selectedWord)
        {
            wordLinked += " _";
        }

        return wordLinked;
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