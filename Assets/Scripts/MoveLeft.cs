using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float destroyBound = -15f; // จุดที่เลยหน้าจอไปแล้วจะให้ทำลายทิ้ง

    // เราจะใช้ static variable เพื่อคุมความเร็วเกมจากที่เดียว (เช่น พอยิ่งเล่นนาน ยิ่งวิ่งเร็วขึ้น)
    public static float GlobalSpeedMultiplier = 1f;

    void Update()
    {
        // ถ้า Game Over แล้ว ไม่ต้องขยับ
        // (คุณอาจต้องเชื่อมกับ GameManager ของคุณภายหลัง ตรงนี้ใส่ comment ไว้ก่อน)
        // if (GameManager.Instance.IsGameOver) return;

        // ขยับไปทางซ้าย
        transform.Translate(Vector3.left * speed * GlobalSpeedMultiplier * Time.deltaTime);

        // Efficiency Check: ถ้าหลุดจอไปแล้ว ให้ทำลายทิ้งเพื่อคืน Ram
        if (transform.position.x < destroyBound)
        {
            Destroy(gameObject);
        }
    }
}