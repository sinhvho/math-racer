using UnityEngine;
using System.Collections;
using TMPro;

namespace MathRacer
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float countdownTime = 60f;
        [SerializeField] private TextMeshProUGUI text;

        private void OnEnable()
        {
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGameReset += OnGameReset;
            GameEvents.OnGamePrepare += OnGamePrepare;
        }

        private void OnDisable()
        {
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGameReset -= OnGameReset;
            GameEvents.OnGamePrepare -= OnGamePrepare;
        }

        private void OnGameStart()
        {
            StartCoroutine(StartCountdown(countdownTime));
            text.gameObject.SetActive(true);
        }

        private void OnGamePrepare()
        {
            text.gameObject.SetActive(false);
        }

        private void OnGameReset()
        {
            StopAllCoroutines();
        }

        private IEnumerator StartCountdown(float time)
        {
            float remainingTime = time;

            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                text.text = ((int)remainingTime).ToString();
                yield return null;
            }

            GameEvents.GameOver();
        }
    }
}
