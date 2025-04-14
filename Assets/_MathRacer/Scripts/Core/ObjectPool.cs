using UnityEngine;
using System.Collections.Generic;

namespace MathRacer
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("Object Pool Parameters")]
        public List<GameObject> PooledObjects { get; private set; } // For now using a list to expand the pool. Can utilize Queue in the future
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;
        [SerializeField] private bool isExpandable;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            PooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, transform);
                tmp.SetActive(false);
                PooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!PooledObjects[i].activeInHierarchy)
                {
                    return PooledObjects[i];
                }
            }
            if (isExpandable)
            {
                GameObject obj = Instantiate(objectToPool);
                obj.SetActive(true);
                PooledObjects.Add(obj);
                return obj;
            }

            return null;
        }
    }
}
