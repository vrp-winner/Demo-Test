using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isInverted = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Wow Factor: Shape Shifting & Gravity Flip
    public void OnGravityShift(InputAction.CallbackContext context)
    {
        if (context.started) // ทำงานตอนคลิก
        {
            ToggleGravity();
        }
    }

    private void ToggleGravity()
    {
        isInverted = !isInverted;

        // กลับค่า Gravity ของ Rigidbody
        rb.gravityScale *= -1;

        // กลับด้าน Sprite
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        transform.localScale = newScale;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            // คำนวณทิศทางการกระโดดตาม Gravity ณ ตอนนั้น
            float direction = isInverted ? -1f : 1f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * direction);
        }
    }

    // Ground Check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;

        // ถ้าชนสิ่งกีดขวาง -> Game Over
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // เรียกใช้ GameManager ให้จบเกม
            GameManager.Instance.GameOver();

            // (Optional) อาจจะสั่งปิดตัวละคร หรือเล่นเสียงระเบิดตรงนี้
            // gameObject.SetActive(false); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}