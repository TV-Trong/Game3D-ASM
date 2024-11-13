using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooler : MonoBehaviour
{
    public static ObjectsPooler instance;
    public List<ObjectPool> objectPools = new List<ObjectPool>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        foreach (ObjectPool pool in objectPools)
        {
            for (int i = 0; i < pool.maxObjectsNumber; i++)
            {
                GameObject objectPool = Instantiate(pool.objectsPrefab);
                objectPool.transform.SetParent(pool.objectsParent, true);
                objectPool.SetActive(false);
                pool.listObject.Add(objectPool);
            }
        }
    }

    public GameObject GetPooledObjects(int index)
    {
        for (int i = 0; i < objectPools[index].listObject.Count; i++)
        {
            if (!objectPools[index].listObject[i].activeInHierarchy) return objectPools[index].listObject[i];
        }
        return null;
    }
}

[System.Serializable]
public class ObjectPool
{
    public GameObject objectsPrefab;
    public int maxObjectsNumber;
    public Transform objectsParent;
    [HideInInspector] public List<GameObject> listObject = new List<GameObject>();
}