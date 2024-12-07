using ASM19301;
using UnityEngine;

public class PlayerDamageToEnemy : MonoBehaviour
{
    public GameObject enemy;  // Đối tượng quái mà bạn muốn gây sát thương

    // Update được gọi mỗi frame
    void Update()
    {
        // Kiểm tra khi người chơi nhấn phím L
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Gọi phương thức trừ 10 máu cho quái
            DealDamageToEnemy();
        }
    }

    // Phương thức gây sát thương cho quái
    void DealDamageToEnemy()
    {
        if (enemy != null)
        {
            // Giả sử quái có script EnemyBehaviour và HP của nó
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                // Trừ 10 máu cho quái
                enemyAI.TakeDamage(10f);  // 10f là sát thương, 0f poise damage, false là không chí mạng
            }
        }
    }
}
