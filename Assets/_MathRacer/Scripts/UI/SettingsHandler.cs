using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MathRacer
{
    public class SettingsHandler : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button quitBtn;
        [SerializeField] private Button settingsBtn;

        [SerializeField] private Canvas settingsCanvas;
        [SerializeField] private RectTransform settingsTransform;

        private void Start()
        {
            sfxSlider?.onValueChanged.AddListener(OnSfxValueChanged);
            bgmSlider?.onValueChanged.AddListener(OnBgmValueChanged);
            settingsBtn?.onClick.AddListener(Pause);

            resumeBtn?.onClick. AddListener(Resume);
            quitBtn?.onClick.AddListener(Quit);
        }

        private void OnDestroy()
        {
            sfxSlider?.onValueChanged.RemoveListener(OnSfxValueChanged);
            bgmSlider?.onValueChanged.RemoveListener(OnBgmValueChanged);
            settingsBtn?.onClick.RemoveListener(Pause);

            resumeBtn?.onClick.RemoveListener(Resume);
            quitBtn?.onClick.RemoveListener(Quit);
        }

        void Pause()
        {
            GameEvents.PauseGame();
            settingsCanvas.gameObject.SetActive(true);
            settingsTransform.DOScale(1, 0.25f).SetUpdate(UpdateType.Normal,true).SetEase(Ease.OutBounce);
        }

        void Quit()
        {
            settingsTransform.DOScale(0, 0.25f).OnComplete(() => {
                settingsCanvas.gameObject.SetActive(false);
                GameEvents.ResetGame();
            }).SetUpdate(UpdateType.Normal, true).SetEase(Ease.InBounce);
        }

        void Resume()
        {
            settingsTransform.DOScale(0, 0.15f).OnComplete(()=> {
                settingsCanvas.gameObject.SetActive(false);
                GameEvents.ResumeGame();
            }).SetUpdate(UpdateType.Normal, true).SetEase(Ease.OutCubic); 
        }

        void OnBgmValueChanged(float value)
        {
            UIEvents.ChangeBgmVolume(value);
        }

        void OnSfxValueChanged(float value)
        {
            UIEvents.ChangeSfxVolume(value);
        }

    }
}