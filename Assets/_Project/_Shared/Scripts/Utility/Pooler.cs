using System;
using System.Collections.Generic;
using UnityEngine;

public class Pooler<TEnum> : MonoBehaviour where TEnum : Enum
{
    [Serializable]
    public class Pool
    {
        public TEnum Type;
        public GameObject Prefab;
        public int PoolSize;
    }

    [Header("---Parent---")]
    protected Transform parent;

    public List<Pool> pools;
    protected Dictionary<TEnum, Queue<GameObject>> poolDictionary;

    public virtual void Awake()
    {
        parent = this.transform;
    }

    protected virtual void Start()
    {
        poolDictionary = new Dictionary<TEnum, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            GameObject typeParent = new($"{pool.Type} Pool");
            if (parent != null)
            {
                typeParent.transform.SetParent(parent);
            }

            Queue<GameObject> objectQueue = new();
            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject prefabObject = Instantiate(pool.Prefab);
                if (typeParent != null)
                {
                    prefabObject.transform.SetParent(typeParent.transform, true);
                }

                prefabObject.SetActive(false);
                objectQueue.Enqueue(prefabObject);
            }

            poolDictionary.Add(pool.Type, objectQueue);
        }
    }

    public virtual GameObject SpawnFromPool(TEnum type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Pool of type {type} does not exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[type].Dequeue();

        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        objectToSpawn.SetActive(true);

        poolDictionary[type].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public virtual void DespawnToPool(GameObject objectToDespawn)
    {
        objectToDespawn.SetActive(false);
    }
}
