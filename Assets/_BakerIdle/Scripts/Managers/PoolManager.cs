using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;

        [Serializable]
        public struct Pool
        {
            public Queue<Item> PooledObjects;
            public List<Item> usedPooledObjects;
            public Item objectPrefab;
            public int poolSize;
        }

        [SerializeField] public Pool[] pools = null;


        private void Awake()
        {
            Instance = this;

            for (int i = 0; i < pools.Length; i++)
            {
                pools[i].PooledObjects = new Queue<Item>();
                pools[i].usedPooledObjects = new List<Item>();

                for (int j = 0; j < pools[i].poolSize; j++)
                {
                    Item obj = Instantiate(pools[i].objectPrefab, transform);
                    obj.gameObject.SetActive(false);
                    pools[i].PooledObjects.Enqueue(obj);
                }
            }

        }

        public Item GetPoolObject(int objectType)
        {
            if (objectType >= pools.Length) return null;

            if (pools[objectType].PooledObjects.Count == 0)
                AddSizePool(5f, objectType);

            Item obj = pools[objectType].PooledObjects.Dequeue();
            pools[objectType].usedPooledObjects.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void SetPoolObject(Item pooledObject, int objectType)
        {
            if (objectType >= pools.Length) return;
            pools[objectType].usedPooledObjects.Remove(pooledObject);
            pools[objectType].PooledObjects.Enqueue(pooledObject);
            pooledObject.gameObject.SetActive(false);

        }

        public void AddSizePool(float amount, int objectType)
        {
            for (int i = 0; i < amount; i++)
            {
                Item obj = Instantiate(pools[objectType].objectPrefab, transform);
                obj.gameObject.SetActive(false);
                pools[objectType].PooledObjects.Enqueue(obj);
            }
        }

        public void DisableAllObjects()
        {
            Debug.Log("DisableAllObjects()");
            for (int i = 0; i < pools[0].usedPooledObjects.Count; i++)
            {
                if (pools[0].usedPooledObjects.Count == 0)
                {
                    return;
                }

                SetPoolObject(pools[0].usedPooledObjects[0], 0);

            }
        }
    }
}