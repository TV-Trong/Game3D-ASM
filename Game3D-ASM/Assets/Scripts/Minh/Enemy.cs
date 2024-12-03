using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private PlayerBehaviour mPlayerBehavior;
    private GameObject mPlayer;
    private BoxCollider mCollision;
    private bool isAttacking;
    private Animator mAnimator;

    void Awake()
    {
        try
        {
            mPlayerBehavior = FindObjectOfType<PlayerBehaviour>();
            mPlayer = GameObject.Find("Asuna");
        }
        catch (System.Exception)
        {

            throw new System.Exception("Player khong ton tai");
        }

        mCollision = GetComponent<BoxCollider>();
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        ChasingPlayer();
        CheckDistancePlayer();
    }

    void ChasingPlayer()
    {
        Vector3 direction = mPlayerBehavior.transform.position - transform.position;
        direction.y = 0; // Giữ hướng trên mặt phẳng (x, z), không ảnh hưởng bởi trục y
        transform.rotation = Quaternion.LookRotation(direction);

        // Di chuyển nhân vật tới mPlayer
        transform.position = Vector3.MoveTowards(transform.position, mPlayerBehavior.transform.position, speed * Time.deltaTime);
    }

    void CheckDistancePlayer()
    {
        if (Vector3.Distance(this.transform.position, mPlayer.transform.position) == .5f)
        {
            isAttacking = true;
            mAnimator.SetBool("isAttacking", isAttacking);
        }
        else
        {
            isAttacking = false;
            mAnimator.SetBool("isAttacking", isAttacking);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touch player");
            mPlayerBehavior.TakeDamage(.5f, 0, false);
            this.gameObject.SetActive(false);
        }
    }
}