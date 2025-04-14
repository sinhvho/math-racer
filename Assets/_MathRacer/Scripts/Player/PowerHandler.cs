using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MathRacer
{
    public class PowerHandler : MonoBehaviour
    {
        [SerializeField] private Slider powerSlider;
        [SerializeField] private float maxPower;
        [SerializeField] private float powerCount;
        [SerializeField] private float powerCost;

        private void OnEnable()
        {
            PlayerEvents.OnPowerCollected += OnPowerCollected;
            UIEvents.OnPowerPressed += OnPowerPressed;
            GameEvents.OnGameReset += OnGameReset;
        }

        private void OnDisable()
        {
            PlayerEvents.OnPowerCollected -= OnPowerCollected;
            UIEvents.OnPowerPressed -= OnPowerPressed;
            GameEvents.OnGameReset -= OnGameReset;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnPowerPressed();
            }
        }

        private void OnPowerCollected()
        {
            if (powerCount < maxPower)
                powerCount++;

            powerSlider.DOValue(powerCount / powerCost,0.3f);
        }

        private void OnPowerPressed()
        {
            if (powerCount < powerCost) return;

            powerCount -= powerCost;
            powerSlider.DOValue(powerCount / powerCost, 0.3f);
            PlayerEvents.UsePower();
        }

        private void OnGameReset()
        {
            powerCount = 0;
            powerSlider.value = 0;
        }
    }
}
