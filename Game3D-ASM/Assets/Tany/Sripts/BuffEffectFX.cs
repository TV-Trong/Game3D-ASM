using UnityEngine;

public class BuffEffectFX : MonoBehaviour
{
    public GameObject buffFXPrefab; // Prefab của hiệu ứng FX
    public float buffDuration; // Thời gian tồn tại của buff (giây)
    private float buffTimer; // Biến theo dõi thời gian đã trôi qua của buff

    public float healthIncrease; // Tỷ lệ tăng máu 
    public float attackIncrease; // Tỷ lệ tăng sức tấn công 
    private float originalHealth; // Lưu trữ lượng máu ban đầu
    private float originalAttack; // Lưu trữ sức tấn công ban đầu
    public float manaCost; //lượng mana cần để kích hoạt
    private bool isBuffing;

    private GameObject buffFXInstance; // Lưu trữ GameObject của hiệu ứng FX

    void Start()
    {
        // Lưu trữ lượng máu và sức tấn công ban đầu
        originalHealth = GetComponent<PlayerBehaviour>().maxHP;
        originalAttack = GetComponent<PlayerBehaviour>().baseStrenght;
    }

    void Update()
    {
        // Kiểm tra xem người chơi có nhấn phím F hay không
        if (Input.GetKeyDown(KeyCode.F) && buffTimer <= 0f)
        {
            ApplyBuff();
            isBuffing = true;
        }

        // Cập nhật thời gian đã trôi qua của buff
        if (buffTimer > 0f)
        {
            buffTimer -= Time.deltaTime;
            // Kiểm tra xem buff đã hết thời gian hay chưa
            if (buffTimer <= 0f)
            {
                RemoveBuff();
                isBuffing = false;
            }
        }

        if (isBuffing)
        {
            GetComponent<PlayerBehaviour>().strength = originalAttack * (1 + attackIncrease);
        }
    }

    public void ApplyBuff()
    {
        if (GetComponent<PlayerBehaviour>().MP >= manaCost)
        {
            // Trừ mana của nhân vật
            GetComponent<PlayerBehaviour>().ConsumeMana(manaCost);

            // Tạo bản sao của hiệu ứng FX
            buffFXInstance = Instantiate(buffFXPrefab, transform.position, Quaternion.Euler(90, 0, 0));

            //đi theo nhân vật
            buffFXInstance.transform.parent = transform;

            // Bắt đầu phát hiệu ứng FX
            ParticleSystem particleSystem = buffFXInstance.GetComponent<ParticleSystem>();
            particleSystem.Play();

            // Tăng máu và sức tấn công
            GetComponent<PlayerBehaviour>().maxHP = originalHealth * (1 + healthIncrease);
            GetComponent<PlayerBehaviour>().HP = GetComponent<PlayerBehaviour>().maxHP;
            GetComponent<PlayerBehaviour>().strength = originalAttack * (1 + attackIncrease);

            // Khởi tạo thời gian đã trôi qua của buff
            buffTimer = buffDuration;
        }
    }

    public void RemoveBuff()
    {
        Destroy(buffFXInstance);

        // Khôi phục máu và sức tấn công
        GetComponent<PlayerBehaviour>().maxHP = originalHealth;
        GetComponent<PlayerBehaviour>().strength = originalAttack;

        buffTimer = 0f;
    }
}