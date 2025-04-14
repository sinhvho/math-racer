using System.Collections;
using UnityEngine;

namespace MathRacer
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 180;
        [SerializeField] private float speed = 3;
        [SerializeField] private float thrustForce = 5;
        [SerializeField] private float slowDuration = 1;
        [SerializeField] private bool isSlowed = false;
        [SerializeField] private Transform startTransform;

        private float defaultSpeed = 8;
        private float defaultThrust = 12;

        private void OnEnable()
        {
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameReset += OnGameReset;
            PlayerEvents.OnPlayerFail += OnPlayerFail;
        }

        private void OnDisable()
        {
            PlayerEvents.OnPlayerFail -= OnPlayerFail;
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGameReset -= OnGameReset;
        }

        private void Start()
        {
            defaultSpeed = speed;
            defaultThrust = thrustForce;
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameManager.GameState.Playing) return;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position += transform.up * thrustForce * Time.deltaTime;
            }
            else
                transform.position += transform.up * speed * Time.deltaTime;

            // Look towards mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector3 direction = (mousePos - transform.position).normalized;

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        private void ResetSettings()
        {
            speed = defaultSpeed;
            thrustForce = defaultThrust;  
        }

        private void OnGameStart()
        {
            ResetSettings();
        }

        private void OnGameOver()
        {
            speed = 0;
            thrustForce = 0;
        }

        private void OnGameReset()
        {
            transform.localPosition = startTransform.position;
            transform.localEulerAngles = Vector3.zero;
        }

        private void OnPlayerFail()
        {
            if (!isSlowed)
            {
                isSlowed = true;
                StartCoroutine(SlowDownCar());
            }
        }

        private IEnumerator SlowDownCar()
        {
            speed = 0.2f;
            thrustForce = 0.2f;
            yield return new WaitForSeconds(slowDuration);
            speed = defaultSpeed;
            thrustForce = defaultThrust;
            isSlowed = false;
            yield return null;
        }

    }
}
