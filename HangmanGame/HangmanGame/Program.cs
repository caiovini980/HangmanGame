using HangmanGame.Managers;
using HangmanGame.ENUMs;

namespace HangmanGame
{
    class Program
    {
        private InputManager _inputManager = new InputManager();
        private GameManager _gameManager = new GameManager();

        private void SetupManagers()
        {
            _inputManager.Setup();
            _gameManager.Setup();
        }

        private void ExecuteSolution()
        {
            Console.WriteLine("Welcome to the Hangman game!");
            Console.WriteLine("1 - Start game");
            Console.WriteLine("0 - Quit game");
            ConsoleKeyInfo key = Console.ReadKey();
            
            if (_inputManager.IsInputValid(key, GameStates.Menu))
            {
                Console.Clear();
                Console.WriteLine("Key pressed is {0}\n", key.Key);
                _gameManager.Play();
            }
            else
            {
                Console.WriteLine("\nPlease select an available option.\nPress any key to continue...\n");
                Console.ReadKey();
                Console.Clear();
                ExecuteSolution();
            }
        }
        
        static int Main(string[] args)
        {
            Program solution = new Program();
            solution.SetupManagers();
            
            // Running
            solution.ExecuteSolution();
            
            return 0;
        }
    }
}