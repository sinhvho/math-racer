using DG.Tweening;
using UnityEngine;

namespace MathRacer
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get { return instance; } }
        private static AudioManager instance;

        [Header("Object Reference")]
        [SerializeField] private AudioSource audioSource;

        [Header("Game Clips")]
        [SerializeField] private AudioClip gameBgm;
        [SerializeField] private AudioClip introBgm;

        private float defaultVolume;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
            GameEvents.OnGameOver += OnGameOver;
            UIEvents.OnBgmVolumeChanged += OnBgmVolumeChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
            GameEvents.OnGameOver -= OnGameOver;

            UIEvents.OnBgmVolumeChanged -= OnBgmVolumeChanged;
        }

        private void Awake()
        {
            if (instance == null && instance != this)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(Instance);
        }
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            defaultVolume = 1;
        }

        private void TransitionBgm(AudioClip clip)
        {
            if (clip == null || audioSource == null) return;

            if (audioSource.clip == clip)
            {
                audioSource.DOFade(defaultVolume, .5f);
                return;
            }

            audioSource.DOFade(0, .5f).OnComplete(() =>
            {
                audioSource.clip = clip;
                audioSource.Play();
                audioSource.DOFade(1, .5f);
            });
        }

        private void TransitionToMute()
        {
            audioSource.DOFade(0, .5f).OnComplete(() =>
            {
                audioSource.clip = null;
            });
        }


        private void OnGameOver()
        {
            TransitionToMute();
        }

        private void OnBgmVolumeChanged(float value)
        {
            audioSource.volume = value;
            defaultVolume = value;
        }

        private void OnGameStateChanged(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.MainMenu:
                    TransitionBgm(introBgm);
                    break;
                case GameManager.GameState.Playing:
                    TransitionBgm(gameBgm);
                    break;
                case GameManager.GameState.Preparing:
                    TransitionToMute();
                    break;
            }
        }
    }
}
