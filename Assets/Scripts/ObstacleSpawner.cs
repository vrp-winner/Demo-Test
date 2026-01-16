using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] obstaclePrefabs; // ใส่ได้หลายแบบ
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 2f;

    [Header("Position Settings")]
    [SerializeField] private float topSpawnY = 3.5f;    // พิกัด Y สำหรับสิ่งกีดขวางด้านบน (เพดาน)
    [SerializeField] private float bottomSpawnY = -3.5f; // พิกัด Y สำหรับสิ่งกีดขวางด้านล่าง (พื้น)

    private bool isSpawning = true;

    void Start()
    {
        // เริ่มลูปการ Spawn
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            // รอเวลาแบบสุ่ม
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // เลือก Prefab แบบสุ่ม
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefabToSpawn = obstaclePrefabs[randomIndex];

        // สุ่มว่าจะเกิด บน หรือ ล่าง (50/50)
        // เพื่อบังคับให้ผู้เล่นต้องใช้สกิล Gravity Shift
        bool isTop = Random.value > 0.5f;
        float spawnY = isTop ? topSpawnY : bottomSpawnY;

        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, 0);

        // สร้าง object -> Instantiate
        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // ถ้าอยู่ด้านบน จะกลับหัวสิ่งกีดขวาง เพื่อให้ดูสมจริง
        if (isTop)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, -obj.transform.localScale.y, obj.transform.localScale.z);
        }
    }
}