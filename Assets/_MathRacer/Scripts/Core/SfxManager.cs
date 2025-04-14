using UnityEngine;

namespace MathRacer
{
    public class SfxManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip correctClip;
        [SerializeField] private AudioClip wrongClip;
        [SerializeField] private AudioClip collectableClip;
        [SerializeField] private AudioClip powerClip;
        [SerializeField] private AudioClip gameOverClip;
        [SerializeField] private AudioClip countdownClip;

        private float defaultVolume;

        private void OnEnable()
        {
            PlayerEvents.OnPlayerSuccess += OnPlayerSuccess;
            PlayerEvents.OnPlayerFail += OnPlayerFail;
            PlayerEvents.OnPowerCollected += OnItemCollected;
            PlayerEvents.OnPowerUsed += OnPowerUsed;

            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGamePrepare += OnGamePrepare;
            GameEvents.OnGamePause += OnGamePause;
            GameEvents.OnGameResume += OnGameResume;
            GameEvents.OnGameReset += OnGameReset;

            UIEvents.OnSfxVolumeChanged += OnSfxVolumeChanged;
        }

        private void OnDisable()
        {
            PlayerEvents.OnPlayerSuccess -= OnPlayerSuccess;
            PlayerEvents.OnPlayerFail -= OnPlayerFail;
            PlayerEvents.OnPowerCollected -= OnItemCollected;
            PlayerEvents.OnPowerUsed -= OnPowerUsed;

            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGamePrepare -= OnGamePrepare;
            GameEvents.OnGamePause -= OnGamePause;
            GameEvents.OnGameResume -= OnGameResume;
            GameEvents.OnGameReset -= OnGameReset;

            UIEvents.OnSfxVolumeChanged -= OnSfxVolumeChanged;
        }
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            defaultVolume = 1;
        }

        private void PlayAudio(AudioClip clip, bool isOneshot=true)
        {
            if (audioSource == null || clip == null) return;

            audioSource.clip = clip;
            if(isOneshot)
                audioSource.PlayOneShot(clip);
            else
                audioSource.Play();
        }

        private void OnPlayerSuccess()
        {
            PlayAudio(correctClip);
        }
        private void OnPlayerFail()
        {
            PlayAudio(wrongClip);
        }

        private void OnItemCollected()
        {
            PlayAudio(collectableClip);
        }

        private void OnPowerUsed()
        {
            PlayAudio(powerClip);
        }

        private void OnGameOver()
        {
            PlayAudio(gameOverClip);
        }

        private void OnGamePrepare()
        {
            PlayAudio(countdownClip,false);
        }

        private void OnGamePause()
        {
            audioSource.Pause();
        }

        private void OnGameResume()
        {
            audioSource.Play();
        }

        private void OnGameReset()
        {
            audioSource.Stop();
            audioSource.volume = defaultVolume;
            audioSource.time = 0;
        }

        private void OnSfxVolumeChanged(float value)
        {
            audioSource.volume = value;
            defaultVolume = value;
        }
    }
}
