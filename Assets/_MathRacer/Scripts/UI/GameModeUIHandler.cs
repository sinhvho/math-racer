using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MathRacer
{
    public class GameModeUIHandler : MonoBehaviour
    {
        [SerializeField] private Button addModeBtn;
        [SerializeField] private Button subtractModeBtn;
        [SerializeField] private Button multiplyModeBtn;
        [SerializeField] private Button divideModeBtn;

        private void Start()
        {
            addModeBtn?.onClick.AddListener(Addition);
            subtractModeBtn?.onClick.AddListener(Subtraction);
            multiplyModeBtn?.onClick.AddListener(Multiplication);
            divideModeBtn?.onClick.AddListener(Division);
        }

        private void OnDestroy()
        {
            addModeBtn?.onClick.RemoveListener(Addition);
            subtractModeBtn?.onClick.RemoveListener(Subtraction);
            multiplyModeBtn?.onClick.RemoveListener(Multiplication);
            divideModeBtn?.onClick.RemoveListener(Division);
        }

        private void Addition()
        {
            GameManager.Instance.SetGameMode(GameManager.GameMode.Addition);
            Play();
        }

        private void Subtraction()
        {
            GameManager.Instance.SetGameMode(GameManager.GameMode.Subtraction);
            Play();
        }

        private void Multiplication()
        {
            GameManager.Instance.SetGameMode(GameManager.GameMode.Multiplication);
            Play();
        }

        private void Division()
        {
            GameManager.Instance.SetGameMode(GameManager.GameMode.Division);
            Play();
        }

        // Utilize UniTask or async in the future for loading scenes asynchronously.
        private void Play()
        {
            TransitionController.Instance.PlayTransition(Vector3.zero);
            DOVirtual.DelayedCall(1.5f, () =>
            {
                GameManager.Instance.PrepareGame();
            });

        }

    }
}