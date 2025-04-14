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
            addModeBtn.onClick.AddListener(() => { GameManager.Instance.SetGameMode(GameManager.GameMode.Addition); Play(); });
            subtractModeBtn.onClick.AddListener(() => { GameManager.Instance.SetGameMode(GameManager.GameMode.Subtraction); Play(); });
            multiplyModeBtn.onClick.AddListener(() => { GameManager.Instance.SetGameMode(GameManager.GameMode.Multiplication); Play(); });
            divideModeBtn.onClick.AddListener(() => { GameManager.Instance.SetGameMode(GameManager.GameMode.Division); Play(); });
        }

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