using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    int count = 0;
    public GameObject portal;

    void Update()
    {
        CountEnemy();
        if (count <= 0)
        {
            portal.SetActive(true);
        }    
    }

    void CountEnemy()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        count = objectsWithTag.Length;
    }    
}
