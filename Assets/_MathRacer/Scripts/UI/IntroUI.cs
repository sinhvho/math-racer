using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathRacer
{
    public class IntroUI : MonoBehaviour
    {
        [SerializeField] private Canvas introCanvas;
        [SerializeField] private Button startBtn;
        [SerializeField] private TextMeshProUGUI logoText;

        private void OnEnable()
        {
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGameReset += OnGameReset;
            GameEvents.OnGamePrepare += OnGameStart;
        }

        private void OnDisable()
        {
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGameReset -= OnGameReset;
            GameEvents.OnGamePrepare -= OnGameStart;
        }
        void Start()
        {
                startBtn?.onClick.AddListener(PrepareGame);

            if(logoText != null)
                logoText.transform.DOScale(1.15f, 1).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
                startBtn?.onClick.RemoveListener(PrepareGame);
        }

        private void OnGameStart()
        {
            introCanvas.gameObject.SetActive(false);
        }

        private void OnGameReset()
        {
            introCanvas.gameObject.SetActive(true);
        }

        private void PrepareGame()
        {
            GameManager.Instance.PrepareGame();
        }
    }
}
