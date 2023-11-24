using HangmanGame.CustomEventArgs;
using HangmanGame.Managers;
using HangmanGame.ENUMs;

namespace HangmanGame
{
    class Program
    {
        // Delegates
        public delegate void GameStartedEventHandler(object source, GameStartEventArgs args);
        public delegate void SolutionInitializedHandler(object source, EventArgs args);
        
        // Events
        public event GameStartedEventHandler? GameStarted;
        public event SolutionInitializedHandler? SolutionInitialized;

        private readonly GameManager _gameManager;
        private readonly InputManager _inputManager;
        private readonly WordsManager _wordsManager;

        // Methods
        Program()
        {
            _gameManager = new GameManager();
            _inputManager = new InputManager();
            _wordsManager = new WordsManager();
        }
        
        private void SetupEvents()
        {
            SolutionInitialized += _gameManager.OnSolutionInitialized;
            SolutionInitialized += _inputManager.OnSolutionInitialized;
            SolutionInitialized += _wordsManager.OnSolutionInitialized;

            GameStarted += _gameManager.OnGameStarted;
        }

        private void ExecuteSolution()
        {
            Console.WriteLine("\nWelcome to the Hangman game!");
            Console.WriteLine("1 - Start game");
            Console.WriteLine("0 - Quit game");
            ConsoleKeyInfo key = Console.ReadKey();
            
            if (_inputManager.IsInputValid(key, GameStates.Menu))
            {
                Console.Clear();
                if (StartGameSelected(key))
                {
                    StartGame();
                }
                
                if (QuitGameSelected(key))
                {
                    CloseSolution();
                }
            }
            else
            {
                Console.WriteLine("\nPlease select an available option.\nPress any key to continue...\n");
                Console.ReadKey();
                Console.Clear();
                ExecuteSolution();
            }
        }

        private void StartGame()
        {
            string gameWord = _wordsManager.GetRandomWord();
            char[] wordLetters = _wordsManager.GetLettersFromWord(gameWord);

            GameStarted?.Invoke(this, new GameStartEventArgs(wordLetters, _inputManager));
            ExecuteSolution();
        }

        private void CloseSolution()
        {
            Console.Clear();
            Console.WriteLine("Closing game...");
            Environment.Exit(0);
        }
        
        private bool QuitGameSelected(ConsoleKeyInfo key)
        {
            return key.KeyChar.ToString() == MenuOptions.CloseGame.ToString();
        }

        private bool StartGameSelected(ConsoleKeyInfo key)
        {
            return key.KeyChar.ToString() == ((int)MenuOptions.StartGame).ToString();
        }
        
        static int Main()
        {
            Program solution = new Program();
            solution.SetupEvents();
            solution.SolutionInitialized?.Invoke(solution, EventArgs.Empty);
            
            // Running
            solution.ExecuteSolution();
            return 0;
        }
    }
}