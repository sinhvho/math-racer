using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

namespace MathRacer
{
    public class GameUI : MonoBehaviour
    {
        [Header("Text Elements")]
        [SerializeField] private TextMeshProUGUI equationText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gameOverScoreText;
        [SerializeField] private TextMeshProUGUI countdownTimer;

        [Header("UI Panels")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject gameOverCanvas;

        [Header("UI Buttons")]
        [SerializeField] private Button powerBtn;
        [SerializeField] private Button boostBtn;
        [SerializeField] private Button continueBtn;

        [SerializeField] private RectTransform gameOverRect;
        [SerializeField] private RectTransform starDropdownRect;

        private UnityAction powerAction;
        private UnityAction boostAction;
        private UnityAction continueAction;

        private Vector2 hiddenPosition;
        private Vector2 visiblePosition = Vector2.zero;

        private Coroutine countdownRoutine;

        private void OnEnable()
        {
            GameManager.OnEquationChanged += OnEquationChanged;
            GameManager.OnScoreChanged += OnScoreChanged;
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameReset += OnGameReset;
            GameEvents.OnGamePrepare += OnGamePrepare;
        }

        private void OnDisable()
        {
            GameManager.OnEquationChanged -= OnEquationChanged;
            GameManager.OnScoreChanged -= OnScoreChanged;
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGameReset -= OnGameReset;
            GameEvents.OnGamePrepare -= OnGamePrepare;
        }

        private void Start()
        {

            powerAction = () => UIEvents.PowerButtonClicked();
            boostAction = () => UIEvents.BoostButtonClicked();
            continueAction = () => GameEvents.ResetGame();

            powerBtn?.onClick.AddListener(powerAction);

            boostBtn?.onClick.AddListener(boostAction);


            continueBtn?.onClick.AddListener(continueAction);

            float screenHeight = Screen.height;
            hiddenPosition = new Vector2(0, screenHeight);

            ResetGameOverPanels();
        }

        private void OnDestroy()
        {
            powerBtn?.onClick.RemoveListener(powerAction);
            boostBtn?.onClick.RemoveListener(boostAction);
            continueBtn?.onClick.RemoveListener(continueAction);
        }

        private void ResetGameOverPanels()
        {
            if (gameOverRect != null)
                gameOverRect.anchoredPosition = hiddenPosition * 2;

            if (starDropdownRect != null)
                starDropdownRect.anchoredPosition = hiddenPosition * 2;
        }

        private void OnEquationChanged(MathProblem problem)
        {
            equationText.text = problem.ToString();
        }

        private void OnScoreChanged(int score)
        {
            scoreText.text = $"Score: {score}";
            gameOverScoreText.text = $"Score: {score}";
        }

        private void OnGameStart()
        {
            equationText.gameObject.SetActive(true);
        }

        private void OnGameOver()
        {
            gamePanel.gameObject.SetActive(false);
            countdownTimer.gameObject.SetActive(false);
            gameOverCanvas.SetActive(true);
            OpenGameOverDropdownPanel();
        }

        private void OnGameReset()
        {
            gamePanel.gameObject.SetActive(false);
            countdownTimer.gameObject.SetActive(false);
            gameOverRect.DOAnchorPos(hiddenPosition, 0f);
            starDropdownRect.DOAnchorPos(hiddenPosition*2, 0f);

            if (countdownRoutine != null)
                StopCoroutine(countdownRoutine);
        }

        private void OnGamePrepare()
        {
            if (countdownRoutine != null)
                StopCoroutine(countdownRoutine);

            countdownRoutine = StartCoroutine(StartCountdown());

            gamePanel.gameObject.SetActive(true);
            equationText.gameObject.SetActive(false);
        }

        private void OpenGameOverDropdownPanel()
        {
            gameOverRect.DOAnchorPos(visiblePosition, 1f)
                .SetEase(Ease.OutCubic).OnComplete(() => {
                    OpenStarDropdownPanel();
                });
        }

        private void OpenStarDropdownPanel()
        {
            starDropdownRect.DOAnchorPos(visiblePosition, 1f);
        }

        private void CloseGameOverDropdownPanel()
        {
            gameOverRect.DOAnchorPos(hiddenPosition, 0.5f)
                .SetEase(Ease.InCubic);
        }

        private void AnimateCountdown(int count)
        {
            countdownTimer.transform.localScale = Vector3.zero;
            countdownTimer.text = count.ToString();
            countdownTimer.transform.DOScale(1, 1);
        }

        // This shouldn't be here, move to TimeHandler in the future.
        private IEnumerator StartCountdown()
        {
            int count = 3;
            countdownTimer.gameObject.SetActive(true);
            while (count > 0)
            {
                AnimateCountdown(count);
                yield return new WaitForSeconds(1);
                count--;
            }
            countdownTimer.gameObject.SetActive(false);

            GameEvents.StartGame();
            yield return null;
        }
    }
}
