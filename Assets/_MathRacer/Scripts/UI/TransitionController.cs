using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace MathRacer
{
    public class TransitionController : MonoBehaviour
    {
        public enum TransitionType
        {
            Circle,
            RoundedSquare
        }

        public enum TransitionPositionType
        {
            Center,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            TopMiddle,
            BottomMiddle,
            LeftMiddle,
            RightMiddle,
            CustomScreenPosition // used for mouse/touch input
        }

        private static TransitionController instance;
        public static TransitionController Instance => instance;

        [Header("Object References")]
        private Material activeMat;
        [SerializeField] private Material circleTransitionMat;
        [SerializeField] private Material roundedSquareTransitionMat;
        [SerializeField] private Canvas canvas;
        [SerializeField] private RawImage transitionImage;

        [Header("Start Position")]
        [SerializeField] private TransitionPositionType startTransitionType = TransitionPositionType.Center;
        [SerializeField] private TransitionPositionType endTransitionType = TransitionPositionType.Center;
        private Vector2 customScreenPosition = Vector2.zero; // used if CustomScreenPosition is selected

        [Header("Settings")]
        [SerializeField] private float transitionDuration = 1f;
        [SerializeField] private float maxRadius = 1.5f;
        [SerializeField] private float cornerRadius = 0.05f;
        [SerializeField] private TransitionType transitionType;
        [SerializeField] private Color[] transitionColors = new Color[4];

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            // Clone the material so we don't affect the original asset
            if(circleTransitionMat != null)
                circleTransitionMat = Instantiate(circleTransitionMat);

            if (roundedSquareTransitionMat != null)
                roundedSquareTransitionMat = Instantiate(roundedSquareTransitionMat);

            if(transitionImage != null)
                transitionImage.enabled = false;
        }

        private void Start()
        {
            ResetTransition();
        }

        /// <summary>
        /// Play circle transition: white expands from click, then reveal expands from center
        /// </summary>
        /// <param name="screenStartPos">Where to start the white circle from (screen position)</param>
        /// <param name="onWhiteFilled">Callback after white fill</param>
        /// <param name="onRevealComplete">Callback after reveal is done</param>
        public void PlayTransition(Vector2 screenStartPos, System.Action onWhiteFilled = null, System.Action onRevealComplete = null)
        {
            if (transitionImage == null || canvas == null)
                return;

            if (transitionImage != null)
                transitionImage.enabled = true;

            activeMat = transitionType == TransitionType.Circle ? circleTransitionMat : roundedSquareTransitionMat;
            transitionImage.material = activeMat;

            // Convert screen point to UV (0-1 range)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenStartPos,
                canvas.worldCamera,
                out Vector2 localPoint);

            Vector2 canvasSize = ((RectTransform)canvas.transform).sizeDelta;
            Vector2 startUv = GetUV(startTransitionType);
            Vector2 endUv = GetUV(endTransitionType);

            activeMat.SetFloat("_Reveal", 0f); // Fill mode
            activeMat.SetFloat("_Radius", 0f);
            activeMat.SetVector("_Center", new Vector4(startUv.x, startUv.y, 0, 0));
            activeMat.SetColor("_Color", transitionColors[Random.Range(0, transitionColors.Length - 1)]);

            if (transitionType == TransitionType.RoundedSquare && activeMat.HasProperty("_CornerRadius"))
                activeMat.SetFloat("_CornerRadius", cornerRadius);

            DOTween.To(() => 0f, r => activeMat.SetFloat("_Radius", r), maxRadius, transitionDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    onWhiteFilled?.Invoke();

                    activeMat.SetFloat("_Reveal", 1f); // Reveal mode
                    activeMat.SetVector("_Center", new Vector4(endUv.x, endUv.y, 0, 0));
                    activeMat.SetFloat("_Radius", 0f);

                    DOTween.To(() => 0f, r => activeMat.SetFloat("_Radius", r), maxRadius, transitionDuration)
                        .SetEase(Ease.OutCubic)
                        .OnComplete(() =>
                        {
                            onRevealComplete?.Invoke();

                            if (transitionImage != null)
                                transitionImage.enabled = false;
                        });
                });
        }

        public void ResetTransition()
        {
            if (transitionImage != null)
                transitionImage.enabled = false;

            activeMat = transitionImage.material;

            if (activeMat == null)
                return;

            activeMat.SetFloat("_Radius", 0f);
            activeMat.SetFloat("_Reveal", 0f);
            activeMat.SetColor("_Color", Color.white);
            activeMat.SetVector("_Center", new Vector4(0.5f, 0.5f, 0, 0));

            if (transitionType == TransitionType.RoundedSquare && activeMat.HasProperty("_CornerRadius"))
                activeMat.SetFloat("_CornerRadius", cornerRadius);
        }

        private Vector2 GetUV(TransitionPositionType type)
        {
            switch (type)
            {
                case TransitionPositionType.TopLeft: return new Vector2(0f, 1f);
                case TransitionPositionType.TopRight: return new Vector2(1f, 1f);
                case TransitionPositionType.BottomLeft: return new Vector2(0f, 0f);
                case TransitionPositionType.BottomRight: return new Vector2(1f, 0f);
                case TransitionPositionType.TopMiddle: return new Vector2(0.5f, 1f);
                case TransitionPositionType.BottomMiddle: return new Vector2(0.5f, 0f);
                case TransitionPositionType.LeftMiddle: return new Vector2(0f, 0.5f);
                case TransitionPositionType.RightMiddle: return new Vector2(1f, 0.5f);
                case TransitionPositionType.CustomScreenPosition:
                    return ScreenToUV(customScreenPosition);
                case TransitionPositionType.Center:
                default: return new Vector2(0.5f, 0.5f);
            }
        }
        private Vector2 ScreenToUV(Vector2 screenPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPos,
                canvas.worldCamera,
                out Vector2 localPoint);

            Vector2 canvasSize = ((RectTransform)canvas.transform).sizeDelta;
            return (localPoint + canvasSize * 0.5f) / canvasSize;
        }
    }
}
