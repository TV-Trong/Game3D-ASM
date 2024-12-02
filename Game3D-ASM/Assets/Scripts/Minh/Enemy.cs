using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform spawnPos;

    private PlayerBehaviour mPlayerBehavior;

    void Awake()
    {
        try
        {
            mPlayerBehavior = FindObjectOfType<PlayerBehaviour>();
        }
        catch (System.Exception)
        {

            throw new System.Exception("Player khong ton tai");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touch player");
            mPlayerBehavior.TakeDamage(4, 0, false);
            this.gameObject.SetActive(false);
        }

        if(other.CompareTag("End"))
        {
            Debug.Log("End");
            this.gameObject.SetActive(false);
        }
    }
}