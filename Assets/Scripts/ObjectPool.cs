using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private Dictionary<string, Queue<GameObject>> objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
    public int poolSize = 10; // Default size, can be modified in the Inspector

    private void Awake()
    {
        // Singleton pattern to ensure only one instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to create a pool for a specific object type
    public void CreatePool(string poolKey, GameObject prefab)
    {
        if (!objectPoolDictionary.ContainsKey(poolKey))
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObj = Instantiate(prefab);
                newObj.name = prefab.name;
                newObj.SetActive(false);
                objectQueue.Enqueue(newObj);
            }

            objectPoolDictionary.Add(poolKey, objectQueue);
        }
    }

    // Method to get an object from the pool
    public GameObject GetObjectFromPool(string poolKey)
    {
        if (objectPoolDictionary.ContainsKey(poolKey))
        {
            GameObject obj = objectPoolDictionary[poolKey].Dequeue();
            obj.SetActive(true);
            objectPoolDictionary[poolKey].Enqueue(obj);
            return obj;
        }
        else
        {
            Debug.LogWarning("Pool with key " + poolKey + " doesn't exist.");
            return null;
        }
    }

    // Method to return an object back to the pool
    public void ReturnObjectToPool(string poolKey, GameObject obj, float delay) => StartCoroutine(ReturnToPoolAfterDelay(poolKey, obj, delay));
    public void ReturnObjectToPool(string poolKey, GameObject obj)
    {
        if (objectPoolDictionary.ContainsKey(poolKey))
        {
            obj.SetActive(false);
            objectPoolDictionary[poolKey].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Pool with key " + poolKey + " doesn't exist.");
        }
    }
    private IEnumerator ReturnToPoolAfterDelay(string poolKey, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObjectToPool(poolKey, obj);
    }
}
