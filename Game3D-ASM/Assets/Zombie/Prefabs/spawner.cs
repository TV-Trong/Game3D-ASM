using UnityEngine;

namespace ASM19301
{
    public class Spawner : MonoBehaviour
    {
        [Header("Prefab Settings")]
        public GameObject prefabToSpawn; // Prefab được chọn để spawn
        public Transform spawnPoint; // Vị trí spawn (mặc định là vị trí của object này)
        public float spawnInterval = 2f; // Khoảng thời gian giữa các lần spawn

        [Header("Spawn Control")]
        public int maxSpawnCount = -1; // Giới hạn số lượng spawn (-1 nghĩa là không giới hạn)
        private int currentSpawnCount = 0;

        private float spawnTimer = 0f;

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            // Spawn prefab khi đạt thời gian yêu cầu
            if (spawnTimer >= spawnInterval)
            {
                SpawnPrefab();
                spawnTimer = 0f;
            }
        }

        private void SpawnPrefab()
        {
            // Kiểm tra giới hạn số lượng spawn (nếu có)
            if (maxSpawnCount != -1 && currentSpawnCount >= maxSpawnCount)
                return;

            // Spawn prefab
            Vector3 spawnPosition = spawnPoint ? spawnPoint.position : transform.position;
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            currentSpawnCount++;
        }
    }
}
