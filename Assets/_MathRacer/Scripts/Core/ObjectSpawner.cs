using UnityEngine;

namespace MathRacer
{
    public class ObjectSpawner : MonoBehaviour
    {

        [Header("Pool References")]
        [SerializeField] private ObjectPool numberPool;
        [SerializeField] private ObjectPool collectiblePool;

        [Header("Randomizing Parameters")]
        [SerializeField] private float minRadius;
        [SerializeField] private float minOffset;
        [SerializeField] private float maxOffset;


        private void OnEnable()
        {
            GameEvents.OnGamePrepare += OnGamePrepare;
        }

        private void OnDisable()
        {
            GameEvents.OnGamePrepare -= OnGamePrepare;
        }

        private void OnGamePrepare()
        {
            SpawnObjects();
        }
        private void SpawnObjects()
        {
            GenerateNumberObjects();
            GeneratePowerObjects();
        }

        void GenerateNumberObjects()
        {
            foreach (GameObject obj in numberPool.PooledObjects)
            {
                obj.transform.position = GetRandomValidPosition(minRadius, minOffset, maxOffset);
                obj.SetActive(true);
            }
        }

        void GeneratePowerObjects()
        {
            foreach (GameObject obj in collectiblePool.PooledObjects)
            {
                obj.transform.position = GetRandomValidPosition(minRadius, minOffset, maxOffset);
                obj.SetActive(true);
            }
        }
        /// <summary>
        /// Gets random positions in bounds but away from the origin within minRadius
        /// </summary>
        /// <param name="minRadius">The minimum away from the origin</param>
        /// <param name="maxX">Maximum distance in the x direction</param>
        /// <param name="maxY">Maximum distance in the y direction</param>
        /// <returns></returns>
        Vector2 GetRandomValidPosition(float minRadius, float maxX, float maxY)
        {
            Vector2 position;

            do
            {
                float x = Random.Range(-maxX, maxX);
                float y = Random.Range(-maxY, maxY);
                position = new Vector2(x, y);
            } while (position.magnitude < minRadius); // magnitude = distance from origin

            return position;
        }
    }
}
