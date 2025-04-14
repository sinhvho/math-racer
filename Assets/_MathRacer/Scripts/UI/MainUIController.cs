using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathRacer {
    public class MainUIController : MonoBehaviour
    {

        [Header("Object References")]
        [SerializeField] private RectTransform mainUiTransform;
        [SerializeField] private RectTransform bottomUiTransform;
        [SerializeField] private RectTransform topUiTransform;

        [Header("Top Navbar")]
        [SerializeField] private float topShowNavOffset = 0f;
        [SerializeField] private float topHideNavOffset = 105f;

        [Header("Right Navbar")]
        [SerializeField] private float rightShowNavOffset = 0f;
        [SerializeField] private float rightHideNavOffset = 250f;

        [Header("Bottom Navbar")]
        [SerializeField] private float botShowNavOffset = -15f;
        [SerializeField] private float botHideNavOffset = -760f;

        [Header("Transition")]
        [SerializeField] private float transitionDuration = 0.5f;

        [SerializeField] private RectTransform adventureUi;
        [SerializeField] private RectTransform gameModesUi;
        [SerializeField] private RectTransform booksUi;
        [SerializeField] private RectTransform videosUi;

        [SerializeField] private Button adventureBtn;
        [SerializeField] private Button gameModesBtn;
        [SerializeField] private Button booksBtn;
        [SerializeField] private Button videosBtn;

        [SerializeField] private Button controlsBtn;
        [SerializeField] private Button closeControlsBtn;

        [SerializeField] private TextMeshProUGUI adventureText;
        [SerializeField] private TextMeshProUGUI gameModesText;
        [SerializeField] private TextMeshProUGUI booksText;
        [SerializeField] private TextMeshProUGUI videosText;

        [SerializeField] private RectTransform selectionBackground;

        private void OnEnable()
        {
            GameEvents.OnGameReset += OnGameReset;
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGamePrepare += OnGamePrepare;
        }

        private void OnDisable()
        {
            GameEvents.OnGameReset -= OnGameReset;
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGamePrepare += OnGamePrepare;
        }

        private void Start()
        {
                adventureBtn?.onClick.AddListener(OnAdventureButtonClicked);
                gameModesBtn?.onClick.AddListener(OnGameModeButtonClicked);
                booksBtn?.onClick.AddListener(OnBookButtonClicked);
                videosBtn?.onClick.AddListener(OnVideoButtonClicked);
                controlsBtn?.onClick.AddListener(ShowBottomNavigation);
                closeControlsBtn?.onClick.AddListener(HideBottomNavigation);
        }

        private void OnDestroy()
        {
                adventureBtn?.onClick.RemoveListener(OnAdventureButtonClicked);
                gameModesBtn?.onClick.RemoveListener(OnGameModeButtonClicked);
                booksBtn?.onClick.RemoveListener(OnBookButtonClicked);
                videosBtn?.onClick.RemoveListener(OnVideoButtonClicked);
                controlsBtn?.onClick.RemoveListener(ShowBottomNavigation);
                closeControlsBtn?.onClick.RemoveListener(HideBottomNavigation);
        }

        private void OnAdventureButtonClicked()
        {
            DisableSelectionUI();

            adventureUi.gameObject.SetActive(true);
            adventureText.color = Color.black;
            selectionBackground.DOAnchorPosY(adventureBtn.transform.localPosition.y, 0.15f).SetEase(Ease.OutSine);
        }

        private void OnBookButtonClicked()
        {
            DisableSelectionUI();
            booksUi.gameObject.SetActive(true);
            booksText.color = Color.black;
            selectionBackground.DOAnchorPosY(booksBtn.transform.localPosition.y, 0.15f).SetEase(Ease.OutSine);
        }

        private void OnGameModeButtonClicked()
        {
            DisableSelectionUI();

            gameModesText.color = Color.black;
            gameModesUi.gameObject.SetActive(true);

            selectionBackground.DOAnchorPosY(gameModesBtn.transform.localPosition.y,0.15f);
        }

        private void OnVideoButtonClicked()
        {
            DisableSelectionUI();

            videosText.color = Color.black;
            videosUi.gameObject.SetActive(true);
            selectionBackground.DOAnchorPosY(videosBtn.transform.localPosition.y, 0.15f);
        }



        private void DisableSelectionUI()
        {
            adventureUi.gameObject.SetActive(false);
            gameModesUi.gameObject.SetActive(false);
            booksUi.gameObject.SetActive(false);
            videosUi.gameObject.SetActive(false);

            adventureText.color = Color.white;
            booksText.color = Color.white;
            videosText.color = Color.white;
            gameModesText.color = Color.white;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O)) { ShowMainUI(); }
            else if (Input.GetKeyDown(KeyCode.P)) { HideMainUI(); }
            else if (Input.GetKeyDown(KeyCode.K)) { ShowBottomNavigation(); }
            else if (Input.GetKeyDown(KeyCode.L)) { HideBottomNavigation(); }
            else if (Input.GetKeyDown(KeyCode.N)) { ShowAllNavigations(); } 
            else if (Input.GetKeyDown(KeyCode.M)) { HideAllNavigations(); }
            else if (Input.GetKeyDown(KeyCode.V)) { ShowTopNavigation(); }
            else if (Input.GetKeyDown(KeyCode.B)) { HideTopNavigation(); }
        }

        private void HideMainUI()
        {
            if (!mainUiTransform)
            {
                Debug.LogError($"The {nameof(mainUiTransform)} variable of the {nameof(MainUIController)} has not been assigned.");
                return;
            }

            mainUiTransform.DOAnchorPosX(rightHideNavOffset, transitionDuration).SetEase(Ease.InCubic);
            adventureUi.DOAnchorPosX(250, transitionDuration).SetEase(Ease.InCubic);
        }

        private void ShowMainUI()
        {
            if (!mainUiTransform)
            {
                Debug.LogError($"The {nameof(mainUiTransform)} variable of the {nameof(MainUIController)} has not been assigned.");
                return;
            }

            mainUiTransform.DOAnchorPosX(rightShowNavOffset, transitionDuration, false).SetEase(Ease.OutBounce);
            adventureUi.DOAnchorPosX(25, transitionDuration).SetEase(Ease.OutBounce);
        }

        private void ShowBottomNavigation()
        {
            if (!mainUiTransform)
            {
                Debug.LogError($"The {nameof(bottomUiTransform)} variable of the {nameof(MainUIController)} has not been assigned.");
                return;
            }

            bottomUiTransform.DOAnchorPosY(botShowNavOffset, transitionDuration, false);
        }

        private void HideBottomNavigation()
        {
            if (!mainUiTransform)
            {
                Debug.LogError($"The {nameof(bottomUiTransform)} variable of the {nameof(MainUIController)} has not been assigned.");
                return;
            }

            bottomUiTransform.DOAnchorPosY(botHideNavOffset, transitionDuration, false);
        }

        private void ShowTopNavigation()
        {
            if (!topUiTransform)
            {
                Debug.LogError($"The {nameof(topUiTransform)} variable of the {nameof(MainUIController)} has not been assigned.");
                return;
            }

            topUiTransform.DOAnchorPosY(topShowNavOffset, transitionDuration, false);
        }

        private void HideTopNavigation()
        {
            if (!topUiTransform)
            {
                Debug.LogError($"The {nameof(topUiTransform)} variable of the {nameof(MainUIController)} has not been assigned.");
                return;
            }

            topUiTransform.DOAnchorPosY(topHideNavOffset, transitionDuration, false);
        }

        private void HideAllNavigations()
        {
            HideTopNavigation();
            HideBottomNavigation();
            HideMainUI();
        }

        private void ShowAllNavigations()
        {
            ShowTopNavigation();
            ShowBottomNavigation();
            ShowMainUI();
        }

        private void HideAllUI()
        {
            gameModesUi.gameObject.SetActive(false);
            videosUi.gameObject.SetActive(false);
            booksUi.gameObject.SetActive(false);
        }

        private void OnGameStart()
        {
            HideAllNavigations();
            HideAllUI();
        }

        private void OnGamePrepare()
        {
            HideAllNavigations();
            HideAllUI();
        }

        private void OnGameReset()
        {
            ShowTopNavigation();
            ShowMainUI();
            OnAdventureButtonClicked();
        }
    }
}
