using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace MathRacer
{
    public class GameModeButton : MonoBehaviour
    {
        private Button button;

        private bool isShaking = false;

        [SerializeField] private float duration;
        [SerializeField] private float strength;
        [SerializeField] private int vibrato;
        [SerializeField] private float randomness;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (isShaking) return;

            isShaking = true;

            button.transform.DOShakeScale(duration, strength, vibrato, randomness, true, ShakeRandomnessMode.Harmonic).OnComplete(() =>
            {
                isShaking = false;
            });
        }
    }
}
