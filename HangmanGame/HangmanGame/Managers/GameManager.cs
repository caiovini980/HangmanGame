using System.Text.RegularExpressions;
using HangmanGame.CustomEventArgs;
using HangmanGame.ENUMs;
using HangmanGame.Interfaces;
using HangmanGame.Utils;

namespace HangmanGame.Managers;

public class GameManager : ManagerBase, IGameMessages, IFileMessages
{
    // Variables
    private string[]? _hangImages;
    private string _wordPlaceholder = IGameMessages.EmptyString;
    
    // File Variables
    private readonly string _cantFindPathErrorMessage = IFileMessages.CantFindHangmanFileMessage;
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
        Console.WriteLine(IGameMessages.InitializationMessage);
        _hangImages = FileUtils.GetSeparatedStrings(_separator, _path);
    }

    private void Play(char[] splittedWord, InputManager inputManager)
    {
        if (!HaveAllHangmanImages()) return;
        
        List<char> usedLetters = new List<char>();
        
        int numberOfUniqueLetters = splittedWord.Distinct().Count();
        int lettersFoundPerTurn = 1;
        int numberOfCorrectLetters = 0;
        int numberOfErrors = 0;
        int maxNumberOfErrors = _hangImages!.Length - 1;

        bool isGameOver = false;
        bool haveWon = false;
        
        SetPlaceholders(splittedWord);
        
        while (!isGameOver)
        {
            bool winConditionReached = numberOfCorrectLetters >= numberOfUniqueLetters;
            bool loseConditionReached = numberOfErrors >= maxNumberOfErrors;
            
            ShowHangman(numberOfErrors);
            ShowUsedLetters(usedLetters);

            if (winConditionReached)
            {
                Console.WriteLine(IGameMessages.FoundAllLettersMessage, _wordPlaceholder);
                isGameOver = true;
                haveWon = true;
                continue;
            }
            
            if (loseConditionReached)
            {
                Console.WriteLine(IGameMessages.AllHangmanPartsAppearedMessage);
                isGameOver = true;
                haveWon = false;
                continue;
            }
            
            Console.WriteLine(_wordPlaceholder);
            Console.WriteLine(IGameMessages.AskForLetterMessage);
            
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();
            bool isInputValidForRunningGame = inputManager.IsInputValid(inputKeyInfo, GameStates.Running);
            char loweredInput = inputKeyInfo.KeyChar.ToString().ToLower().ToCharArray()[0];
            char usedLetter = usedLetters.Find(x => x == loweredInput);
            char usedLetterFormatted = usedLetter.ToString().ToLower().ToCharArray()[0];
            
            if (!isInputValidForRunningGame)
            {
                Console.WriteLine(IGameMessages.InvalidKeyMessage);
                ShowEndTurnSection();
                continue;
            }

            int[] matchedItemsIndexes = splittedWord.FindAllIndexesOf(loweredInput);
            bool isGuessOnSelectedWord = matchedItemsIndexes.Length > 0;
            bool isGuessANewLetter = usedLetterFormatted == new char();
            
            if (!isGuessANewLetter)
            {
                Console.WriteLine(IGameMessages.RepeatedLetterMessage, usedLetterFormatted);
                ShowEndTurnSection();
                continue;
            }
                
            if (isGuessOnSelectedWord)
            {
                Console.WriteLine(IGameMessages.FoundNewLetterMessage, usedLetterFormatted);
                
                UpdatePlaceholdersWithInputAt(matchedItemsIndexes, loweredInput);
                numberOfCorrectLetters += lettersFoundPerTurn; 
                ShowEndTurnSection();
            }
            else
            {
                Console.WriteLine(IGameMessages.WrongGuessMessage);
                numberOfErrors += 1;
                ShowEndTurnSection();
            }
            
            usedLetters.Add(loweredInput);
        }
        
        ShowEndgameMessage(haveWon);
        Console.WriteLine(IGameMessages.ReturningToMenuMessage);
        ShowEndTurnSection();
    }

    private bool HaveAllHangmanImages()
    {
        if (_hangImages == null)
        {
            Console.Error.WriteLine(_cantFindPathErrorMessage);
            return false;
        }

        return true;
    }

    private void ShowUsedLetters(List<char> usedLetters)
    {
        Console.WriteLine(IGameMessages.UsedLettersSeparatorMessage);
        string usedLettersText = IGameMessages.EmptyString;
        foreach (var letter in usedLetters)
        {
            usedLettersText += letter + IGameMessages.DefaultSeparatorSpaced;
        }

        Console.WriteLine(usedLettersText + IGameMessages.LineBreak);
    }

    private void ShowHangman(int numberOfErrors)
    {
        if (_hangImages == null) { return; }
        Console.WriteLine(Regex.Unescape(_hangImages[numberOfErrors]));
    }

    private void ShowEndTurnSection()
    {
        Console.WriteLine(IGameMessages.ContinueMessage);
        Console.ReadKey();
        Console.Clear();
    }

    private void SetPlaceholders(char[] selectedWord)
    {
        _wordPlaceholder = IGameMessages.EmptyString;
        foreach (char unused in selectedWord)
        {
            _wordPlaceholder += IGameMessages.DefaultSeparator;
        }
    }

    private void UpdatePlaceholdersWithInputAt(int[] indexes, char input)
    {
        char[] auxArray = _wordPlaceholder.ToCharArray();
        _wordPlaceholder = IGameMessages.EmptyString;
        
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
            Console.WriteLine(IGameMessages.WinGameMessage);
            return;
        }
        
        Console.WriteLine(IGameMessages.LoseGameMessage);
    }
}