using System;

namespace MathRacer
{
    public static class PlayerEvents
    {
        public static event Action OnPlayerSuccess;
        public static event Action OnPlayerFail;
        public static event Action OnPowerCollected;
        public static event Action OnPowerUsed;
        public static event Action OnBoostUsed;
        public static event Action<int> OnNumberCollected;

        public static void NumberCollected(int num) => OnNumberCollected?.Invoke(num);

        public static void PowerCollected() => OnPowerCollected?.Invoke();
        public static void PlayerSuccess() => OnPlayerSuccess?.Invoke();
        public static void PlayerFail() => OnPlayerFail?.Invoke();
        public static void UsePower() => OnPowerUsed?.Invoke();
        public static void UseBoost() => OnBoostUsed?.Invoke();

    }
}
