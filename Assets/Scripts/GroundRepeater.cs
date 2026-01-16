using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundRepeater : MonoBehaviour
{
    private float groundWidth;

    void Start()
    {
        // หาความกว้างของพื้นจาก Collider
        groundWidth = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
    }

    void Update()
    {
        // เช็คว่าพื้นหลุดจอไปทางซ้ายหรือยัง (เทียบกับตำแหน่งตัวเอง)
        // ถ้าตำแหน่ง X น้อยกว่า -ความกว้าง (แปลว่าหลุดจอไปทั้งชิ้นแล้ว)
        if (transform.position.x < -groundWidth)
        {
            // ย้ายตำแหน่งไปข้างหน้า (Re-position)
            // สูตร: ย้ายไปข้างหน้าเท่ากับ (ความกว้าง * 2) 
            // *คูณ 2 เพราะจะมีพื้นในฉาก 2 ชิ้นวิ่งสลับกัน
            Vector2 resetPosition = new Vector2(groundWidth * 2f, 0);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}