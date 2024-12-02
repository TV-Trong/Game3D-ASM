using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    void Update()
    {
        StartCoroutine(Spawnn(10));
    }

    IEnumerator Spawnn(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        Instantiate(prefab, this.transform.position, this.transform.rotation);
    }
}