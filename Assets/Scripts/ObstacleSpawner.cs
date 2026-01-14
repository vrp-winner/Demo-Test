using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] obstaclePrefabs; // ใส่ได้หลายแบบ (เช่น กล่องเล็ก, กล่องใหญ่)
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 2f;

    [Header("Position Settings")]
    [SerializeField] private float topSpawnY = 3.5f;    // พิกัด Y สำหรับสิ่งกีดขวางด้านบน (เพดาน)
    [SerializeField] private float bottomSpawnY = -3.5f; // พิกัด Y สำหรับสิ่งกีดขวางด้านล่าง (พื้น)

    private bool isSpawning = true;

    void Start()
    {
        // เริ่มลูปการเสกของ
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            // 1. รอเวลาแบบสุ่ม
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // 2. เลือก Prefab แบบสุ่ม (เผื่อในอนาคตมีหลายรูปทรง)
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefabToSpawn = obstaclePrefabs[randomIndex];

        // 3. สุ่มว่าจะเกิด บน หรือ ล่าง (50/50)
        // เพื่อบังคับให้ผู้เล่นต้องใช้สกิล Gravity Shift
        bool isTop = Random.value > 0.5f;
        float spawnY = isTop ? topSpawnY : bottomSpawnY;

        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, 0);

        // 4. สร้าง object (Instantiate)
        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // **สำคัญมาก**: ถ้าเกิดด้านบน เราต้องกลับหัวสิ่งกีดขวางด้วยเพื่อให้ดูสมจริง
        if (isTop)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, -obj.transform.localScale.y, obj.transform.localScale.z);
        }
    }
}