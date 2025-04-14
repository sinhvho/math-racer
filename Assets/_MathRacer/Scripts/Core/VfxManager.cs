using DG.Tweening;
using UnityEngine;

namespace MathRacer
{
    public class VfxManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private ParticleSystem wowParticle;
        [SerializeField] private ParticleSystem powerParticle;
        [SerializeField] private ParticleSystem directionalConfettiParticle;
        [SerializeField] private ParticleSystem areaStarParticle;
        [SerializeField] private float powerParticleDuration = 0.7f;

        [SerializeField] private Transform player;

        private void OnEnable()
        {
            PlayerEvents.OnPlayerSuccess += OnPlayerSuccess;
            PlayerEvents.OnPlayerFail += OnPlayerFail;
            PlayerEvents.OnPowerUsed += OnPowerUsed;
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameReset += OnGameReset;
        }

        private void OnDisable()
        {
            PlayerEvents.OnPlayerSuccess -= OnPlayerSuccess;
            PlayerEvents.OnPlayerFail -= OnPlayerFail;
            PlayerEvents.OnPowerUsed -= OnPowerUsed;
            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGameReset -= OnGameReset;
        }

        private void OnPlayerSuccess()
        {
            if (!wowParticle) return;

            wowParticle.Stop();
            wowParticle.Play();
        }

        private void OnPlayerFail()
        {
            if (!explosionParticle) return;

            explosionParticle.Stop();
            explosionParticle.Play();
        }

        private void OnPowerUsed()
        {
            if (!powerParticle) return;

            if (player != null)
                powerParticle.transform.position = player.position;

            powerParticle.Stop();
            powerParticle.Play();

            DOVirtual.DelayedCall(powerParticleDuration, () => {
                powerParticle.Stop();
            });
        }

        private void OnGameOver()
        {
            if (!directionalConfettiParticle) return;

            directionalConfettiParticle.Stop(); 
            directionalConfettiParticle.Play();

            DOVirtual.DelayedCall(1.75f, PlayAreaStarParticle);
        }

        private void OnGameReset()
        {
            if (!areaStarParticle) return;

            areaStarParticle.Clear();
            areaStarParticle.Stop();
        }

        private void PlayAreaStarParticle()
        {
            if (!areaStarParticle) return;

            areaStarParticle.Stop();
            areaStarParticle.Play();
        }
    }
}
