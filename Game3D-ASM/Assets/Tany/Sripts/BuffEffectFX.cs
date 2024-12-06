using UnityEngine;

public class BuffEffectFX : MonoBehaviour
{
    public GameObject buffFXPrefab; // Prefab của hiệu ứng FX
    public float buffDuration; // Thời gian tồn tại của buff (giây)
    private float buffTimer; // Biến theo dõi thời gian đã trôi qua của buff

    public float healthIncrease; // Tỷ lệ tăng máu (50%)
    public float attackIncrease; // Tỷ lệ tăng sức tấn công (50%)

    private float originalHealth; // Lưu trữ lượng máu ban đầu
    private float originalAttack; // Lưu trữ sức tấn công ban đầu

    private GameObject buffFXInstance; // Lưu trữ GameObject của hiệu ứng FX

    void Start()
    {
        // Lưu trữ lượng máu và sức tấn công ban đầu
        originalHealth = GetComponent<PlayerBehaviour>().HP; // Giả sử bạn có component Health
        originalAttack = GetComponent<PlayerBehaviour>().strength; // Giả sử bạn có component Attack
    }

    void Update()
    {
        // Kiểm tra xem người chơi có nhấn phím F hay không
        if (Input.GetKeyDown(KeyCode.F) && buffTimer <= 0f)
        {
            ApplyBuff();
        }

        // Cập nhật thời gian đã trôi qua của buff
        if (buffTimer > 0f)
        {
            buffTimer -= Time.deltaTime;
            // Kiểm tra xem buff đã hết thời gian hay chưa
            if (buffTimer <= 0f)
            {
                RemoveBuff();
            }
        }
    }

    public void ApplyBuff()
    {
        // Tạo bản sao của hiệu ứng FX
        buffFXInstance = Instantiate(buffFXPrefab, transform.position, Quaternion.Euler(90, 0, 0));

        //đi theo nhân vật
        buffFXInstance.transform.parent = transform;

        // Bắt đầu phát hiệu ứng FX
        ParticleSystem particleSystem = buffFXInstance.GetComponent<ParticleSystem>();
        particleSystem.Play();

        // Tăng máu và sức tấn công
        GetComponent<PlayerBehaviour>().HP = originalHealth * (1 + healthIncrease);
        GetComponent<PlayerBehaviour>().strength = originalAttack * (1 + attackIncrease);

        // Khởi tạo thời gian đã trôi qua của buff
        buffTimer = buffDuration;
    }

    public void RemoveBuff()
    {
        // Xóa hiệu ứng FX
        Destroy(buffFXInstance);

        // Khôi phục máu và sức tấn công
        GetComponent<PlayerBehaviour>().HP = originalHealth;
        GetComponent<PlayerBehaviour>().strength = originalAttack;

        // Đặt lại thời gian đã trôi qua của buff
        buffTimer = 0f;
    }
}