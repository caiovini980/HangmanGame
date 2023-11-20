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

        private GameManager _gameManager;
        private InputManager _inputManager;
        private WordsManager _wordsManager;

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
                Console.WriteLine("Key pressed is {0}\n", key.Key);
                string gameWord = _wordsManager.GetRandomWord();
                GameStarted?.Invoke(this, new GameStartEventArgs(gameWord));
            }
            else
            {
                Console.WriteLine("\nPlease select an available option.\nPress any key to continue...\n");
                Console.ReadKey();
                Console.Clear();
                ExecuteSolution();
            }
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