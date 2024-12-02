using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private PlayerBehaviour mPlayerBehavior;
    private BoxCollider mCollision;

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

        mCollision = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ChasingPlayer();
    }

    void ChasingPlayer()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, mPlayerBehavior.transform.position, speed * Time.deltaTime);   
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touch player");
            mPlayerBehavior.TakeDamage(.5f, 0, false);
            this.gameObject.SetActive(false);
        }

        if(other.CompareTag("End"))
        {
            Debug.Log("End");
            this.gameObject.SetActive(false);
        }
    }
}