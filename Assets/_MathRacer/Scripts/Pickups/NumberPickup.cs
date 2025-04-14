using UnityEngine;
using TMPro;

namespace MathRacer
{
    public class NumberPickup : MonoBehaviour
    {

        [SerializeField] private int currentValue;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnEnable()
        {
            GameEvents.OnGamePrepare += OnGameStart;
            PlayerEvents.OnPlayerSuccess += OnPlayerSuccess;
            PlayerEvents.OnPowerUsed += OnPowerUsed;
        }

        private void OnDisable()
        {
            GameEvents.OnGamePrepare -= OnGameStart;
            PlayerEvents.OnPlayerSuccess -= OnPlayerSuccess;
            PlayerEvents.OnPowerUsed -= OnPowerUsed;
        }

        private void Start()
        {
            if (spriteRenderer == null) { Debug.LogError($"{nameof(spriteRenderer)} variable of the {nameof(NumberPickup)} has not been assigned."); }

            text = GetComponentInChildren<TextMeshPro>();

            UpdateInfo();
        }

        private void OnPlayerSuccess()
        {
            UpdateInfo();
        }

        private void OnPowerUsed()
        {
            UpdateInfo(GameManager.Instance.Problem.Answer);
        }

        private void OnGameStart()
        {
            UpdateInfo();
        }

        public void UpdateInfo(int num)
        {
            currentValue = num;
            text.text = currentValue.ToString();

            //Random.InitState(currentValue);
            spriteRenderer.color = Random.ColorHSV(0.1f, .7f, 0.2f, 1f, 0.5f, 0.7f);
        }

        private void UpdateInfo()
        {
            MathProblem rndProblem = MathProblemGenerator.GenerateProblem((MathOperation)GameManager.Instance.Mode, 2,5);

            currentValue = rndProblem.Answer;
            text.text = currentValue.ToString();

            //Random.InitState(currentValue);
            spriteRenderer.color = Random.ColorHSV(0.1f, .7f, 0.2f, 1f, 0.5f, 0.7f);
        }

        public void ChangePosition()
        {
            transform.position = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (string.Equals(collider.gameObject.tag, "Player"))
            {
                PlayerEvents.NumberCollected(currentValue);
                ChangePosition();
            }
        }
    }
}
