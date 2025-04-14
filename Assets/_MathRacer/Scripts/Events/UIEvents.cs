using System;

namespace MathRacer {
    public static class UIEvents
    {
        public static event Action OnPowerPressed;
        public static event Action OnBoostPressed;
        public static event Action<float> OnBgmVolumeChanged;
        public static event Action<float> OnSfxVolumeChanged;
        public static void PowerButtonClicked() => OnPowerPressed?.Invoke();
        public static void BoostButtonClicked() => OnBoostPressed?.Invoke();
        public static void ChangeBgmVolume(float value) => OnBgmVolumeChanged?.Invoke(value);
        public static void ChangeSfxVolume(float value) => OnSfxVolumeChanged?.Invoke(value);
    }
}
