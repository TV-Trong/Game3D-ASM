using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    void Start()
    {
        StartCoroutine(SpawnRoutine(1));
    }

    IEnumerator SpawnRoutine(float t)
    {
        while (true)
        {
            Instantiate(prefab, new Vector3(Random.Range(68.7f, 417.4f), 1, Random.Range(97, 355)), transform.rotation);
            yield return new WaitForSecondsRealtime(t);
        }
    }
}
