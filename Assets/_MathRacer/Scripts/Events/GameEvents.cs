using System;

namespace MathRacer
{
    public static class GameEvents
    {
        public static event Action OnGameOver;
        public static event Action OnGameStart;
        public static event Action OnGameReset;
        public static event Action OnGamePrepare;
        public static event Action OnGamePause;
        public static event Action OnGameResume;
        public static void StartGame() => OnGameStart?.Invoke();
        public static void GameOver() => OnGameOver?.Invoke();
        public static void ResetGame() => OnGameReset?.Invoke();
        public static void PrepareGame() => OnGamePrepare?.Invoke();
        public static void PauseGame() => OnGamePause?.Invoke();
        public static void ResumeGame() => OnGameResume?.Invoke();



    }
}
