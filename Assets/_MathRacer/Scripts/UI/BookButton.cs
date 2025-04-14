using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace MathRacer
{
    public class BookButton : MonoBehaviour
    {
        private Button bookButton;

        private bool isShaking = false;

        [SerializeField] private float duration;
        [SerializeField] private float strength;
        [SerializeField] private int vibrato;
        [SerializeField] private float randomness;

        private void Start()
        {
            bookButton = GetComponent<Button>();
            bookButton?.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            bookButton?.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (isShaking) return;

            isShaking = true;

            bookButton.transform.DOShakeRotation(duration, strength, vibrato, randomness, true, ShakeRandomnessMode.Harmonic).OnComplete(() =>
            {
                isShaking = false;
            });
        }
    }
}
