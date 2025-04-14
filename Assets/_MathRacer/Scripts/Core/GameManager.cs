using UnityEngine;
using System;

namespace MathRacer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get { return instance; } }
        private static GameManager instance;
        public enum GameState { MainMenu, Preparing, Playing, Paused, GameOver }
        public enum GameMode { Addition, Subtraction, Multiplication, Division }

        [SerializeField] private GameState gameState;
        private GameState prevState;
        [SerializeField] private GameMode gameMode;

        public GameState State { get { return gameState; } }
        public GameMode Mode { get { return gameMode; } }
        public int Score { get; private set; }
        public MathProblem Problem { get; private set; }

        public static event Action<int> OnScoreChanged;
        public static event Action<MathProblem> OnEquationChanged;
        public static event Action<GameState> OnGameStateChanged;
        public static event Action<GameMode> OnGameModeChanged;

        private void OnEnable()
        {
            PlayerEvents.OnPlayerSuccess += OnPlayerSuccess;
            PlayerEvents.OnNumberCollected += OnNumberCollected;
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameReset += OnGameReset;
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGamePause += OnGamePause;
            GameEvents.OnGameResume += OnGameResume;
        }

        private void OnDisable()
        {
            PlayerEvents.OnPlayerSuccess -= OnPlayerSuccess;
            PlayerEvents.OnNumberCollected -= OnNumberCollected;
            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGameReset -= OnGameReset;
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGamePause -= OnGamePause;
            GameEvents.OnGameResume -= OnGameResume;
        }

        private void Awake()
        {
            if (instance == null && instance != this)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(instance);
            }
        }

        private void Start()
        {
            SetGameState(GameState.MainMenu);
        }

        public void PrepareGame()
        {
            if (State == GameState.Playing || State == GameState.Preparing) return;

            SetNewEquation();
            SetGameState(GameState.Preparing);
            GameEvents.PrepareGame();
        }

        private void SetGameState(GameState newState)
        {
            prevState = gameState;
            gameState = newState;
            OnGameStateChanged?.Invoke(gameState);
        }

        public void SetGameMode(GameMode newMode)
        {
            gameMode = newMode;
            OnGameModeChanged?.Invoke(gameMode);
        }

        private void CheckAnswer(int num)
        {
            if (num == Problem.Answer)
                PlayerEvents.PlayerSuccess();
            else
                PlayerEvents.PlayerFail();
        }
        private void SetNewEquation()
        {
            Problem = MathProblemGenerator.GenerateProblem((MathOperation)gameMode, 2, 5);
            OnEquationChanged?.Invoke(Problem);
        }

        private void IncreaseScore()
        {
            Score++;
            OnScoreChanged?.Invoke(Score);
        }

        private void ResetScore()
        {
            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }

        private void OnNumberCollected(int num)
        {
            CheckAnswer(num);
        }

        private void OnGameOver()
        {
            SetGameState(GameState.GameOver);
        }

        private void OnGameStart()
        {
            SetGameState(GameState.Playing);
        }

        private void OnGameReset()
        {
            ResetScore();
            Time.timeScale = 1;
            SetGameState(GameState.MainMenu);
        }

        private void OnGameResume()
        {
            Time.timeScale = 1;
            SetGameState(prevState);
        }

        private void OnGamePause()
        {
            SetGameState(GameState.Paused);
            Time.timeScale = 0;
        }

        private void OnPlayerSuccess()
        {
            SetNewEquation();
            IncreaseScore();
        }
    }
}
