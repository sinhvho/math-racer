using DG.Tweening;
using UnityEngine;

namespace MathRacer
{
    public class PowerPickup : MonoBehaviour
    {
        private void ChangePosition()
        {
            transform.position = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (string.Equals(collider.gameObject.tag, "Player"))
            {
                transform.DOMove(collider.bounds.center, 0.1f).OnComplete(() =>
                {
                    PlayerEvents.PowerCollected();
                    ChangePosition();
                });
            }
        }
    }
}
