using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPooler : Singleton<BrickPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Pool(string tag, GameObject prefab, int size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }

    public Dictionary<string, int> BrickCounter = new Dictionary<string, int>();
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);   
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        BrickCounter[Value.BLUE_BRICK] = 0;
        BrickCounter[Value.RED_BRICK] = 0;
        BrickCounter[Value.GREEN_BRICK] = 0;
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(poolDictionary[tag].Count <= 0)
        {
            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    poolDictionary[tag].Enqueue(obj);
                }
            }
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        BrickCounterControl(tag, 1);
        return objectToSpawn;
    }

    public void DespawnToPool(string tag, GameObject prefab)
    {
        prefab.SetActive(false);
        poolDictionary[tag].Enqueue(prefab);
        BrickCounterControl(tag, -1);
    }

    private void BrickCounterControl(string tag, int numToAdd)
    {
        BrickCounter[tag] += numToAdd;
    }
}
